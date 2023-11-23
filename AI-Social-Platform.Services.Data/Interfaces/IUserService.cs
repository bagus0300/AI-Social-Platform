using AI_Social_Platform.Data.Models;
using AI_Social_Platform.FormModels;
using AI_Social_Platform.Services.Data.Models.PublicationDtos;

namespace AI_Social_Platform.Services.Data.Interfaces
{
    public interface IUserService
    {
        Task<ApplicationUser> GetUserByIdAsync(string userId);

        Task<bool> EditUserDataAsync(string id, UpdateUserFormModel updatedUserData);
    }
}

