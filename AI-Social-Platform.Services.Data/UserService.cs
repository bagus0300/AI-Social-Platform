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

            ApplicationUser user = await this.dbContext.ApplicationUsers.FindAsync(userId);

            return user;

        }

        public async Task<UserDetailsDto?> GetUserDetailsByIdAsync(string id)
        {
            ApplicationUser user = await this.dbContext
                .ApplicationUsers
                .Include(u => u.Country)
                .Include(u => u.State)
                .Include(u => u.UserSchools)
                .Include(u => u.Friends)
                .FirstAsync(u => u.IsActive && u.Id.ToString() == id);

            List<FriendDetailsDto> friends = await GetUserFriends(user);

            List<SchoolDto> schools = GetUserSchools(user);

            List<PublicationDto> publications = GetUserPublications(user);

            UserDetailsDto userDetailModel = new()
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Country = user.Country.Name,
                State = user.State.Name,
                Gender = user.Gender.ToString(),
                Birthday = user.Birthday,
                Relationship = user.Relationship.ToString(),
                ProfilePictureBase64 = Convert.ToBase64String(user.ProfilePicture!) ?? null,
                CoverPhotoBase64 = Convert.ToBase64String(user.CoverPhoto!) ?? null,
                Friends = friends,
                UserSchools = schools,
                Publications = publications
            };

            return userDetailModel;
        }


        public Task<UserFormModel> GetUserDetailsForEditAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> EditUserDataAsync(string id, UserFormModel updatedUserData)
        {
            Guid userId = Guid.Parse(id);

            var user = await this.dbContext.ApplicationUsers.FindAsync(userId);

            if (user != null)
            {
                string? newStateName = updatedUserData.State;
                State? oldState = await dbContext.States.FirstOrDefaultAsync(s => s.Name == newStateName);

                if (newStateName != null && oldState == null)
                {
                    State state = new State
                    {
                        Name = newStateName
                    };

                    dbContext.States.Add(state);
                    await dbContext!.SaveChangesAsync();
                }

                string? newCountryName = updatedUserData.Country;
                Country? oldCountry = await dbContext.Countries.FirstOrDefaultAsync(c => c.Name == newCountryName);

                if (newCountryName != null && oldCountry == null)
                {
                    Country country = new Country
                    {
                        Name = newCountryName
                    };
                    dbContext.Countries.Add(country);
                    await dbContext!.SaveChangesAsync();
                }

                user.State = await dbContext.States.FirstAsync(s => s.Name == newStateName);
                user.Country = await dbContext.Countries.FirstAsync(c => c.Name == newCountryName);
                user.FirstName = updatedUserData.FirstName;
                user.LastName = updatedUserData.LastName;
                user.PhoneNumber = updatedUserData.PhoneNumber;
                user.Gender = updatedUserData.Gender;
                user.Birthday = updatedUserData.Birthday;
                user.Relationship = updatedUserData.Relationship;

                await this.dbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
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

        public async Task<ICollection<FriendDetailsDto>> GetFriendsAsync(string userId)
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
                    ProfilePictureBase64 = Convert.ToBase64String(friend.ProfilePicture!) ?? null,
                })
                .ToArray();

            return friends;
        }

        public async Task<bool> CheckIfUserExistsAsync(string userEmail) //returns true if user exists
        {
            var user = await dbContext.ApplicationUsers.FirstOrDefaultAsync(u => u.Email == userEmail);
            return user != null;
        }


        private static List<SchoolDto> GetUserSchools(ApplicationUser user)
        {
            List<SchoolDto> schools = new List<SchoolDto>();

            foreach (var school in user.UserSchools)
            {
                SchoolDto schoolDto = new SchoolDto()
                {
                    Id = school.School.Id,
                    Name = school.School.Name,
                    State = school.School.State.Name
                };
                schools.Add(schoolDto);
            }

            return schools;
        }

        private async Task<List<FriendDetailsDto>> GetUserFriends(ApplicationUser user)
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