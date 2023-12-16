namespace AI_Social_Platform.Services.Data.Interfaces
{
    using AI_Social_Platform.Data.Models;
    using AI_Social_Platform.FormModels;
    using Microsoft.AspNetCore.Http;

    public interface IMediaService
    {
        Task UploadMediaAsync(IFormFileCollection files, string userId, bool? isItPublication);

        Task DeleteMediaAsync(string id);

        Task<Media> ReplaceOrEditMediaAsync(string id, MediaFormModel updatedMedia);

        Task<bool> IsUserOwnThedMedia(string userId, string mediaId);

        Task<Media> GetMediaAsync(string mediaId);

        Task<ICollection<Media>> GetAllMediaFilesByUserIdAsync(string userId);

        Task<ICollection<Media>> GetAllMediaByPublicationIdAsync(string publicationId);

        Task<ICollection<Media>> GetAllMediaByUserIdAsync(string userId);
    }
}