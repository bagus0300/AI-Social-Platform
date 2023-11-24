using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AI_Social_Platform.Services.Data.Models
{
    using AI_Social_Platform.Data;
    using AI_Social_Platform.Data.Models;
    using FormModels;
    using Interfaces;        

    public class UserService : IUserService
    {
        private readonly ASPDbContext dbContext;
        private readonly IConfiguration configuration;

        public UserService(ASPDbContext dbContext, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.configuration = configuration;
        }

        
        public string BuildToken(string userEmail)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userEmail),
                
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                this.configuration["Jwt:Issuer"],
                this.configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(1),
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
            else
            {
                ApplicationUser user = await this.dbContext.ApplicationUsers.FindAsync(userId);

                return user;
            }            
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

        
    }
}