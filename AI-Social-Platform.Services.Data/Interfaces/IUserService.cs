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

        Task<bool> AddFriendAsync(ApplicationUser currentUser, string friendId);

        Task<bool> AreFriendsAsync(Guid id, Guid friendId);

        Task<bool> RemoveFriendAsync(ApplicationUser currentUser, string friendId);

        Task<ICollection<FriendDetailsDto>?> GetFriendsAsync(string userId);

        Task<bool> CheckIfUserExistByEmailAsync(string userEmail);

        Task<bool> CheckIfUserExistsByIdAsync(string id);

    }
}