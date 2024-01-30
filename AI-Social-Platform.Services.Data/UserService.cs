namespace AI_Social_Platform.Services.Data.Models
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Http;

    using AI_Social_Platform.Data;
    using AI_Social_Platform.Data.Models;
    using AI_Social_Platform.Data.Models.Enums;
    using FormModels;
    using Interfaces;
    using UserDto;
    using PublicationDtos;
    using System.Net.Http;

    public class UserService : IUserService
    {
        private readonly ASPDbContext dbContext;
        private readonly IConfiguration configuration;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IBaseSocialService baseSocialService;
        private readonly HttpContext httpContext;

        public UserService(ASPDbContext dbContext, IConfiguration configuration,
            UserManager<ApplicationUser> userManager, IBaseSocialService baseSocialService, IHttpContextAccessor httpContext)
        {
            this.dbContext = dbContext;
            this.configuration = configuration;
            this.userManager = userManager;
            this.baseSocialService = baseSocialService;
            this.httpContext = httpContext.HttpContext;
        }

        public string BuildToken(string userId)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                this.configuration["Jwt:Issuer"],
                this.configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("Invalid user data!");
            }

            ApplicationUser user = (await this.dbContext.ApplicationUsers.FindAsync(userId))!;

            return user;

        }

        public async Task<UserDetailsDto?> GetUserDetailsByIdAsync(string id)
        {
            ApplicationUser user = await this.dbContext
                .ApplicationUsers
                .Include(u => u.Country)
                .Include(u => u.State)
                .FirstAsync(u => u.IsActive && u.Id.ToString() == id);

            UserDetailsDto userDetailModel = new()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber ?? null,
                Country = user.Country?.Name ?? null,
                State = user.State?.Name ?? null,
                Gender = user.Gender?.ToString() ?? null,
                Birthday = user.Birthday?.Date ?? null,
                Relationship = user.Relationship?.ToString() ?? null,
                School = user.School ?? null,
                CoverPhotoData = user.CoverPhoto ?? null,
                ProfilePictureData = user.ProfilePicture ?? null
            };

            return userDetailModel;
        }
        
        public async Task<bool> EditUserDataAsync(string id, UserFormModel updatedUserData)
        {
            Guid userId = Guid.Parse(id);

            var user = await this.dbContext.ApplicationUsers.FindAsync(userId);

            if (user != null)
            {
                updatedUserData.State = updatedUserData.State?.TrimEnd();
                updatedUserData.Country = updatedUserData.Country?.TrimEnd();
                if (string.IsNullOrEmpty(updatedUserData.State))
                {
                    user.StateId = null;
                }
                else
                {
                    user.State = await GetOrCreateStateAsync(updatedUserData.State);
                    
                }

                if (string.IsNullOrEmpty(updatedUserData.Country))
                {
                    user.CountryId = null;
                }
                else
                {
                    user.Country = await GetOrCreateCountryAsync(updatedUserData.Country);

                }
                user.FirstName = updatedUserData.FirstName;
                user.LastName = updatedUserData.LastName;
                user.PhoneNumber = updatedUserData.PhoneNumber;
                user.Gender = updatedUserData.Gender;
                user.Birthday = updatedUserData.Birthday;
                user.Relationship = updatedUserData.Relationship;
                user.School = updatedUserData.School;

                if (updatedUserData.ProfilePicture != null)
                {
                    using var memoryStream = new MemoryStream();
                    await updatedUserData.ProfilePicture.CopyToAsync(memoryStream);
                    user.ProfilePicture = memoryStream.ToArray();
                }
                if (updatedUserData.CoverPhoto != null)
                {
                    using var memoryStream = new MemoryStream();
                    await updatedUserData.CoverPhoto.CopyToAsync(memoryStream);
                    user.CoverPhoto = memoryStream.ToArray();
                }

                await this.dbContext.SaveChangesAsync();
                return true;
            }
            
            return false;
        }

        public async Task<bool> AddFriendAsync(Guid friendId)
        {
            var friendShip = await dbContext.Friendships.FirstOrDefaultAsync(f => f.UserId == GetUserId() && f.FriendId == friendId);

            if (friendShip != null)
            {
                return false;
            }
            var friendship = new List<Friendship>()
            {
                new Friendship
                {
                    UserId = GetUserId(),
                    FriendId = friendId
                },
                new Friendship
                {
                    UserId = friendId,
                    FriendId = GetUserId()
                }
            };
           
            await dbContext.Friendships.AddRangeAsync(friendship);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveFriendAsync(Guid friendId)
        {
            var friendShipsUser = await dbContext.Friendships
                .FirstOrDefaultAsync(u => u.FriendId == friendId && u.UserId == GetUserId());

            var friendShipsFriend = await dbContext.Friendships
                .FirstOrDefaultAsync(u => u.FriendId == GetUserId() && u.UserId == friendId);

            if (friendShipsUser == null)
            {
                return false;
            }

            if (friendShipsFriend == null)
            {
                return false;
            }

            dbContext.Friendships.Remove(friendShipsUser);
            dbContext.Friendships.Remove(friendShipsFriend);
           await dbContext.SaveChangesAsync();

           return true;
        }

        public async Task<ICollection<UserDetailsDto>?> GetAllUsers() 
        {
            var users = dbContext.ApplicationUsers
                .Where(u => u.IsActive);

            ICollection<UserDetailsDto> userDto =  await users.Select(u => new UserDetailsDto()
            {
                Id = u.Id,
                UserName = u.UserName,
                FirstName = u.FirstName,
                LastName = u.LastName,
                ProfilePictureData = u.ProfilePicture,

            }).ToArrayAsync();

            return userDto;
        }


        public async Task<ICollection<FriendDetailsDto>?> GetFriendsAsync(Guid userId)
        {
            var friendShips = dbContext.Friendships
                .Where(f => f.UserId == userId)
                .Include(u => u.Friend);

           var friendsDto = await friendShips.Select(f => new FriendDetailsDto()
                {
                    Id = f.FriendId,
                    FirstName = f.Friend.FirstName,
                    LastName = f.Friend.LastName,
                    ProfilePictureData = f.Friend.ProfilePicture
                }).ToListAsync();
           
           return friendsDto;
        }
        

        public async Task<bool> CheckIfUserExistByEmailAsync(string userEmail) //returns true if user exists
        {
            var user = await dbContext.ApplicationUsers.FirstOrDefaultAsync(u => u.Email == userEmail);
            
            return user != null;
        }

        public async Task<bool> CheckIfUserExistsByIdAsync(Guid id)
        {
            var user = await dbContext.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == id);

            return user != null;
        }
        public static List<PublicationDto> GetUserPublications(ApplicationUser user)
        {
            List<PublicationDto> publications = new List<PublicationDto>();

            foreach (var publication in user.Publications)
            {
                PublicationDto publicationDto = new PublicationDto()
                {
                    Id = publication.Id,
                    Content = publication.Content,
                    DateCreated = publication.DateCreated,
                    AuthorId = publication.AuthorId
                };
                publications.Add(publicationDto);
            }

            return publications;
        }


        // Private //

        private async Task<State?> GetOrCreateStateAsync(string stateName)
        {
            if (!string.IsNullOrWhiteSpace(stateName))
            {
                var state = await dbContext.States.FirstOrDefaultAsync(s => s.Name == stateName);
                if (state == null)
                {
                    state = new State
                    {
                        Name = stateName
                    };
                    dbContext.States.Add(state);
                }
                return state;
            }

            return null;
        }
        
        private async Task<Country?> GetOrCreateCountryAsync(string countryName)
        {
            if (!string.IsNullOrWhiteSpace(countryName))
            {
                Country? country = await dbContext.Countries.FirstOrDefaultAsync(c => c.Name == countryName);
                if (country == null)
                {
                    country = new Country
                    {
                        Name = countryName
                    };
                    dbContext.Countries.Add(country);
                }
                return country;
            }

            return null;
        }
        private Guid GetUserId()
        {
            var userId = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!;

            if (userId == null)
            {
                throw new NullReferenceException();
            }

            return Guid.Parse(userId);
        }
    }
}