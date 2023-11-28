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
    using Common;

    public class UserService : IUserService
    {
        private readonly ASPDbContext dbContext;
        private readonly IConfiguration configuration;
        private readonly UserManager<ApplicationUser> userManager;

        public UserService(ASPDbContext dbContext, IConfiguration configuration,
            UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.configuration = configuration;
            this.userManager = userManager;
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


        public async Task<bool> EditUserDataAsync(string id, UpdateUserFormModel updatedUserData)
        {
            Guid userId = Guid.Parse(id);

            var user = await this.dbContext.ApplicationUsers.FindAsync(userId);

            if (user != null)
            {
                user.FirstName = updatedUserData.FirstName;
                user.LastName = updatedUserData.LastName;
                user.PhoneNumber = updatedUserData.PhoneNumber;

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

        public async Task<ICollection<FriendDto>> GetFriendsAsync(string userId)
        {
            var user = await dbContext.ApplicationUsers
                .Include(u => u.Friends)
                .FirstOrDefaultAsync(u => u.Id.ToString() == userId);

            if (user == null)
            {
                return null;
            }

            var friends = user.Friends
                .Select(friend => new FriendDto
                {
                    UserName = friend.UserName,
                    FirstName = friend.FirstName,
                    LastName = friend.LastName
                })
                .ToArray();

            return friends;
        }


    }
}