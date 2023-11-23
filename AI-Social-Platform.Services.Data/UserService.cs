namespace AI_Social_Platform.Services.Data.Models
{
    using AI_Social_Platform.Data;
    using AI_Social_Platform.Data.Models;
    using AI_Social_Platform.FormModels;
    using AI_Social_Platform.Services.Data.Interfaces;        

    public class UserService : IUserService
    {
        private readonly ASPDbContext dbContext;

        public UserService(ASPDbContext dbContext)
        {
            this.dbContext = dbContext;
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