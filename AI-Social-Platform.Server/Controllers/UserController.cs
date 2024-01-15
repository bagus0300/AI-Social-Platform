using AI_Social_Platform.Services.Data.Models.UserDto;

namespace AI_Social_Platform.Server.Controllers
{
    using System.Security.Claims;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.Caching.Memory;

    using Models;
    using FormModels;
    using AI_Social_Platform.Data.Models;
    using AI_Social_Platform.Services.Data.Interfaces;
    using AI_Social_Platform.Services.Data.Models.UserDto;

    using static Common.NotificationMessagesConstants;
    using static Common.GeneralApplicationConstants;
    using static Extensions.ClaimsPrincipalExtensions;
    using static AI_Social_Platform.Common.EntityValidationConstants;


    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserService userService;
        private readonly IMemoryCache memoryCache;


        public UserController(IUserService userService, SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager, IMemoryCache memoryCache)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.userService = userService;
            this.memoryCache = memoryCache;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = ErrorMessage });
            }

            if (await userService.CheckIfUserExistByEmailAsync(model.Email))
            {
                return BadRequest(new { Message = UserAlreadyExists });
            }

            ApplicationUser user = new()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
            };

            await userManager.SetEmailAsync(user, model.Email);
            await userManager.SetUserNameAsync(user, model.Email);

            IdentityResult result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return BadRequest(new { Message = RegistrationFailed, result.Errors });
            }
            await signInManager.SignInAsync(user, isPersistent: false);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            var claimsIdentity = new ClaimsIdentity(claims, "ApplicationCookie");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(claimsPrincipal);

            await signInManager.SignInAsync(user, true);

            string userId = user.Id.ToString();

            this.memoryCache.Remove(UserCacheKey);

            return Ok(new LoginResponse
            {
                Succeeded = true,
                UserId = userId,
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ProfilePicture = GetProfileImageUrl(user.Id),
                Token = userService.BuildToken(userId)
            });
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new LoginResponse { Succeeded = false, ErrorMessage = InvalidLoginData });
            }

            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);

            if (!result.Succeeded)
                return BadRequest(new { message = InvalidLoginData });

            var user = await userManager.FindByEmailAsync(model.Email);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            var claimsIdentity = new ClaimsIdentity(claims, "ApplicationCookie");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(claimsPrincipal);

            await signInManager.SignInAsync(user, true);

            string userId = user.Id.ToString();

            return Ok(new LoginResponse
            {
                Succeeded = true,
                UserId = userId,
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ProfilePicture = GetProfileImageUrl(user.Id),
                Token = userService.BuildToken(userId)
            });
        }


        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return Ok(new { message = "Logged out successfully." });
        }


        [HttpGet("ProfilePicture/{userId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserProfilePicture(string userId)
        {
            var user = await userService.GetUserDetailsByIdAsync(userId);
            if (user?.ProfilePictureData != null)
            {
                return File(user.ProfilePictureData, "image/png");
            }
            return BadRequest("No profile picture found!");
        }


        [HttpGet("CoverPicture/{userId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserCoverPicture(string userId)
        {
            var user = await userService.GetUserDetailsByIdAsync(userId);
            if (user == null)
            {
                return BadRequest("User not found!");
            }
            if (user.CoverPhotoData != null)
            {
                return File(user.CoverPhotoData, "image/png");
            }

            return BadRequest("No cover photo found!");
        }


        [HttpGet("userDetails/{userId}")]
        public async Task<IActionResult> GetUserDetails(string userId)
        {
            
            try
            {
                UserDetailsDto? user = await userService.GetUserDetailsByIdAsync(userId);
                if (user == null)
                {
                    return NotFound(new { message = "Current user not found!"});
                }

                if (user.ProfilePictureData != null)
                {
                    user.ProfilePictureUrl = GetProfileImageUrl(user.Id);
                    user.ProfilePictureData = null;
                }
                if (user.CoverPhotoData != null)
                {
                    user.CoverPhotoUrl = GetCoverImageUrl(user.Id);
                    user.CoverPhotoData = null;
                }

                return Ok(user);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}"});
            }
        }

        
        [HttpPut("updateUser")]
        public async Task<IActionResult> UpdateUserData([FromForm] UserFormModel model)
        {
            var userId = HttpContext.User.GetUserId();

            try
            {
                bool success = await userService.EditUserDataAsync(userId!, model);

                if (success)
                {
                    return Ok(new { message = "User data updated successfully"});
                }
                else
                {
                    return NotFound(new { message = "User not found or not active"});
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}"});
            }
        }
        

        [HttpPost("addFriend/{friendId}")]
        public async Task<IActionResult> AddFriend(string friendId)
        {
            try
            {
                ApplicationUser currentUser = await userManager.GetUserAsync(User);

                if (currentUser == null)
                {
                    return NotFound(new { message = "Current user not found!" });
                }

                if (currentUser.Id.ToString() == friendId)
                {
                    return BadRequest(new { message = "Cannot add yourself as a friends list!" });
                }
                
                bool areFriends = await userService.AreFriendsAsync(currentUser.Id, Guid.Parse(friendId));
                
                if (areFriends)
                {
                    return BadRequest(new { message = "Users are already friends!" });
                }

                bool success = await userService.AddFriendAsync(currentUser!, friendId!);

                if (success)
                {
                    return Ok(new { message = "Friend added successfully." });
                }

                return BadRequest(new { message = "Failed to add friend." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }
        }


        [HttpPost("removeFriend/{friendId}")]
        public async Task<IActionResult> RemoveFriend(string friendId)
        {
            try
            {
                var currentUser = await userManager.GetUserAsync(User);

                if (currentUser == null)
                {
                    return NotFound(new { message = "Current user not found!"});
                }

                var success = await userService.RemoveFriendAsync(currentUser!, friendId!);

                if (success)
                {
                    return Ok(new { message = "Friend removed successfully."});
                }

                return BadRequest(new { message = "Failed to remove friend."});
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}"});
            }
        }


        [HttpGet("allFriends")]
        public async Task<IActionResult> GetAllFriends(string userId)
        {
            try
            {
                if (string.IsNullOrEmpty(userId) )
                {
                    return BadRequest(new { message = "User not found."});
                }


                bool userExist = await userService.CheckIfUserExistsByIdAsync(userId);

                if (!userExist)
                {
                    return BadRequest(new { message = "User not found or wrong data." });
                }

                ICollection<FriendDetailsDto>? friends = await userService.GetFriendsAsync(userId);
                if (friends.Any())
                {
                    foreach (var friend in friends)
                    {
                        friend.ProfilePictureUrl = GetProfileImageUrl(friend.Id);
                    }
                }

                return Ok(friends);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}"});
            }
        }
        

        private string GetProfileImageUrl(Guid userId)
        {
            var request = HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";

            return $"{baseUrl}/api/User/ProfilePicture/{userId}";
        }


        private string GetCoverImageUrl(Guid userId)
        {
            var request = HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";

            return $"{baseUrl}/api/User/CoverPicture/{userId}";
        }
    }

}