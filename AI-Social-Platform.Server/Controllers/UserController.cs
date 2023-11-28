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

    using static Common.NotificationMessagesConstants;
    using static Common.GeneralApplicationConstants;
    using static Extensions.ClaimsPrincipalExtensions;


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

            ApplicationUser user = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
            };

            await this.userManager.SetEmailAsync(user, model.Email);
            await this.userManager.SetUserNameAsync(user, model.Email);

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
            this.memoryCache.Remove(UserCacheKey);
            return Ok(new { Message = SuccessMessage });

        }


        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<LoginResponse> Login(LoginFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return new LoginResponse { Succeeded = false, ErrorMessage = InvalidLoginData };
            }

            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);

            if (!result.Succeeded)
                return new LoginResponse { Succeeded = false, ErrorMessage = LoginFailed };

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

            return new LoginResponse { Succeeded = true, Token = userService.BuildToken(userId) };
        }


        [HttpPut("updateUser")]
        public async Task<IActionResult> UpdateUserData([FromBody] UpdateUserFormModel model)
        {
            var userId = HttpContext.User.GetUserId();

            try
            {
                bool success = await userService.EditUserDataAsync(userId!, model);

                if (success)
                {
                    return Ok("User data updated successfully");
                }
                else
                {
                    return NotFound("User not found or not active");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("addFriend/{friendId}")]
        public async Task<IActionResult> AddFriend(string friendId)
        {
            try
            {
                var currentUser = await userManager.GetUserAsync(User);

                if (currentUser == null)
                {
                    return NotFound("Current user not found!");
                }

                if (currentUser.Id.ToString() == friendId)
                {
                    return BadRequest("Cannot add yourself as a friends list!");
                }

                var success = await userService.AddFriend(currentUser!, friendId!);

                if (success)
                {
                    return Ok("Friend added successfully.");
                }

                return BadRequest("Failed to add friend.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
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
                    return NotFound("Current user not found!");
                }

                var success = await userService.RemoveFriend(currentUser!, friendId!);

                if (success)
                {
                    return Ok("Friend removed successfully.");
                }

                return BadRequest("Failed to remove friend.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("allFriends")]
        public async Task<IActionResult> GetAllFriends()
        {
            try
            {
                var userId = this.User.GetUserId();

                if (string.IsNullOrEmpty(userId))
                {
                    return BadRequest("User not found.");
                }

                var friends = await userService.GetFriendsAsync(userId);

                return Ok(friends);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


    }

}