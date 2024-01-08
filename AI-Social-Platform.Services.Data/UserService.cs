using AI_Social_Platform.Data.Models.Enums;

namespace AI_Social_Platform.Services.Data.Models
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.EntityFrameworkCore;

    using AI_Social_Platform.Data;
    using AI_Social_Platform.Data.Models;
    using FormModels;
    using Interfaces;
    using UserDto;
    using PublicationDtos;

    public class UserService : IUserService
    {
        private readonly ASPDbContext dbContext;
        private readonly IConfiguration configuration;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IBaseSocialService baseSocialService;

        public UserService(ASPDbContext dbContext, IConfiguration configuration,
            UserManager<ApplicationUser> userManager, IBaseSocialService baseSocialService)
        {
            this.dbContext = dbContext;
            this.configuration = configuration;
            this.userManager = userManager;
            this.baseSocialService = baseSocialService;
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
                .Include(u => u.School)
                .Include(u => u.Friends)
                .FirstAsync(u => u.IsActive && u.Id.ToString() == id);

            List<FriendDetailsDto> friends = await GetUserFriendsAsync(user);


            UserDetailsDto userDetailModel = new()
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber ?? null,
                Country = user.Country?.Name ?? null,
                State = user.State?.Name ?? null,
                Gender = user.Gender?.ToString() ?? null,
                Birthday = user.Birthday?.Date ?? null,
                Relationship = user.Relationship?.ToString() ?? null,
                ProfilePictureData = user.ProfilePicture != null ? user.ProfilePicture : null,
                CoverPhotoData = user.CoverPhoto != null ? user.CoverPhoto : null,
                Friends = friends,
                School = user.School?.Name ?? null,
                ProfilPictureUrl = null,
                CoverPhotoUrl = null
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
                
                user.State = await GetOrCreateStateAsync(updatedUserData.State);
                user.Country = await GetOrCreateCountryAsync(updatedUserData.Country);
                user.FirstName = updatedUserData.FirstName;
                user.LastName = updatedUserData.LastName;
                user.PhoneNumber = updatedUserData.PhoneNumber;
                user.Gender = updatedUserData.Gender;
                user.Birthday = updatedUserData.Birthday;
                user.Relationship = updatedUserData.Relationship;

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

        public async Task<bool> AddFriend(ApplicationUser currentUser, string friendId)
        {
            ApplicationUser? friendUser = await userManager.FindByIdAsync(friendId);

            if (friendUser == null)
            {
                return false;
            }

            bool areFriends = currentUser.Friends.Any(f => f.Id.ToString() == friendUser.Id.ToString());

            if (areFriends)
            {
                return false;
            }

            currentUser.Friends.Add(friendUser);

            friendUser.Friends.Add(currentUser);

            await baseSocialService.CreateNotificationAsync(friendUser.Id, 
                currentUser.Id, NotificationType.Follow, null);

            await userManager.UpdateAsync(currentUser);
            await userManager.UpdateAsync(friendUser);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveFriend(ApplicationUser currentUser, string friendId)
        {
            ApplicationUser? friendUser = await userManager.FindByIdAsync(friendId);

            if (friendUser == null)
            {
                return false;
            }

            bool areFriends = currentUser.Friends.Any(f => f.Id.ToString() == friendUser.Id.ToString());

            if (!areFriends)
            {
                return false;
            }

            currentUser.Friends.Remove(friendUser);

            friendUser.Friends.Remove(currentUser);

            await userManager.UpdateAsync(currentUser);
            await userManager.UpdateAsync(friendUser);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<ICollection<FriendDetailsDto>?> GetFriendsAsync(string userId)
        {
            var user = await dbContext.ApplicationUsers
                .Include(u => u.Friends)
                .FirstOrDefaultAsync(u => u.Id.ToString() == userId);

            if (user == null)
            {
                return null;
            }

            var friends = user.Friends
                .Select(friend => new FriendDetailsDto
                {
                    UserName = friend.UserName,
                    FirstName = friend.FirstName,
                    LastName = friend.LastName,
                    Id = friend.Id,
                    ProfilePictureData = friend.ProfilePicture
                })
                .ToArray();

            return friends;
        }

        public async Task<bool> CheckIfUserExistsAsync(string userEmail) //returns true if user exists
        {
            var user = await dbContext.ApplicationUsers.FirstOrDefaultAsync(u => u.Email == userEmail);
            return user != null;
        }

        public async Task<bool> AddUserSchool(ApplicationUser currentUser, SchoolFormModel model)
        {
            if (currentUser.School == null)
            {
                var state = await dbContext.States.FirstOrDefaultAsync(s => s.Name == model.State);
                if (state == null)
                {
                    state = new State
                    {
                        Name = model.State
                    };
                }

                var school = await dbContext.Schools.FirstOrDefaultAsync(s => s.Name == model.Name);
                if (school == null)
                {
                    school = new School
                    {
                        Name = model.Name,
                        StateId = state.Id
                    };
                }

                currentUser.School = school;
                await dbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> EditUserSchool(ApplicationUser currentUser, SchoolFormModel model)
        {
            var state = await dbContext.States.FirstOrDefaultAsync(s => s.Name == model.State);
            if (state == null)
            {
                state = new State
                {
                    Name = model.State
                };
            }

            var school = await dbContext.Schools.FirstOrDefaultAsync(s => s.Name == model.Name);
            if (school == null)
            {
                school = new School
                {
                    Name = model.Name,
                    StateId = state.Id
                };
            }

            currentUser.State = state;
            currentUser.School = school;
            await dbContext.SaveChangesAsync();
            return true;

        }

        public async Task<bool> RemoveUserSchool(ApplicationUser currentUser)
        {
            currentUser.SchoolId = null;
            await dbContext.SaveChangesAsync();
            return true;
        }

        private async Task<State> GetOrCreateStateAsync(string stateName)
        {
            if (!string.IsNullOrWhiteSpace(stateName))
            {
                State state = await dbContext.States.FirstOrDefaultAsync(s => s.Name == stateName);
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

        private async Task<Country> GetOrCreateCountryAsync(string countryName)
        {
            if (!string.IsNullOrWhiteSpace(countryName))
            {
                Country country = await dbContext.Countries.FirstOrDefaultAsync(c => c.Name == countryName);
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

        private async Task<List<FriendDetailsDto>> GetUserFriendsAsync(ApplicationUser user)
        {
            List<FriendDetailsDto> friends = new List<FriendDetailsDto>();

            foreach (var itemFriend in user.Friends)
            {
                var myFriend = await this.dbContext.ApplicationUsers.FindAsync(itemFriend.Id);
                string userName = myFriend?.UserName ?? string.Empty;

                FriendDetailsDto friend = new FriendDetailsDto()
                {
                    FirstName = user.FirstName ?? string.Empty,
                    LastName = user.LastName ?? string.Empty,
                    UserName = userName
                };
                friends.Add(friend);
            }

            return friends;
        }

        private static List<PublicationDto> GetUserPublications(ApplicationUser user)
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
    }
}