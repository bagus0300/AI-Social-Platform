namespace AI_Social_Platform.Services.Data.Interfaces
{
    using AI_Social_Platform.Data.Models;
    using FormModels;
    using Models.UserDto;


    public interface IUserService
    {

        string BuildToken(string userEmail);

        Task<ApplicationUser> GetUserByIdAsync(string userId);

        Task<UserDetailsDto?> GetUserDetailsByIdAsync(string id);
        
        Task<bool> EditUserDataAsync(string id, UserFormModel updatedUserData);

        Task<bool> AddFriend(ApplicationUser currentUser, string friendId);

        Task<bool> RemoveFriend(ApplicationUser currentUser, string friendId);

        Task<ICollection<FriendDetailsDto>?> GetFriendsAsync(string userId);

        Task<bool> CheckIfUserExistsAsync(string userEmail);

        Task<bool> AddUserSchool(ApplicationUser currentUser, SchoolFormModel model);

        Task<bool> EditUserSchool(ApplicationUser currentUser, SchoolFormModel model);

        Task<bool> RemoveUserSchool(ApplicationUser currentUser);
    }
}