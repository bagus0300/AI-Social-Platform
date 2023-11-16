namespace AI_Social_Platform.Services.Data.Interfaces
{
    using AI_Social_Platform.Data.Models;
    using AI_Social_Platform.FormModels;
    using Microsoft.AspNetCore.Http;

    public interface IMediaService
    {
        Task UploadMediaAsync(IFormFile file, string userId);

        Task DeleteMediaAsync(string id);

        Task<Media> ReplaceOrEditMediaAsync(string id, MediaFormModel updatedMedia);

        Task<bool> IsUserOwnThedMedia(string userId, string mediaId);
    }
}




