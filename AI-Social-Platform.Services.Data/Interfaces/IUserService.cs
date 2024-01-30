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

        Task<bool> AddFriendAsync(Guid friendId);

        Task<bool> RemoveFriendAsync(Guid friendId);

        Task<ICollection<FriendDetailsDto>?> GetFriendsAsync(Guid userId);

        Task<ICollection<UserDetailsDto>?> GetAllUsers();

        Task<bool> CheckIfUserExistByEmailAsync(string userEmail);

        Task<bool> CheckIfUserExistsByIdAsync(Guid id);

    }
}