using AI_Social_Platform.Data.Models;
using AI_Social_Platform.FormModels;
using AI_Social_Platform.Services.Data.Models.PublicationDtos;
using AI_Social_Platform.Services.Data.Models.UserDto;

namespace AI_Social_Platform.Services.Data.Interfaces
{
    public interface IUserService
    {
        string BuildToken(string userEmail);

        Task<ApplicationUser> GetUserByIdAsync(string userId);

        Task<bool> EditUserDataAsync(string id, UpdateUserFormModel updatedUserData);

        Task<bool> AddFriend(ApplicationUser currentUser, string friendId);

        Task<bool> RemoveFriend(ApplicationUser currentUser, string friendId);

        Task<ICollection<FriendDto>> GetFriendsAsync(string userId);
    }
}

