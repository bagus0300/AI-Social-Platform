namespace AI_Social_Platform.Services.Data.Interfaces
{
    using AI_Social_Platform.Data.Models;
    using AI_Social_Platform.FormModels;
    using Microsoft.AspNetCore.Http;

    public interface IMediaService
    {
        Task UploadMediaAsync(IFormFileCollection files, string userId);

        Task DeleteMediaAsync(string id);

        Task<Media> ReplaceOrEditMediaAsync(string id, MediaFormModel updatedMedia);

        Task<bool> IsUserOwnThedMedia(string userId, string mediaId);

        Task<Media> GetMediaAync(string mediaId);

        Task<ICollection<Media>> GetAllMediaFilesByUserIdAsync(string userId);

        Task<ICollection<Media>> GetAllMediaFilesByPublicationIdAsync(string publicationId);
    }
}




