namespace AI_Social_Platform.Services.Data
{
    using AI_Social_Platform.Data;
    using AI_Social_Platform.Data.Models;
    using AI_Social_Platform.FormModels;
    using AI_Social_Platform.Services.Data.Interfaces;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;

    public class MediaService : IMediaService
    {
        private readonly ASPDbContext dbContext;
        public MediaService(ASPDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task UploadMediaAsync(IFormFile file, string userId)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);

                byte[] fileBytes = memoryStream.ToArray();

                Media fileToUpload = new Media()
                {
                    UserId = Guid.Parse(userId),
                    DataFile = fileBytes
                };

                await dbContext.MediaFiles.AddAsync(fileToUpload);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<Media> ReplaceOrEditMediaAsync(string id, MediaFormModel updatedMedia)
        {
            Media? existingMedia = await dbContext
               .MediaFiles
               .Where(m => m.IsDeleted == false)
               .FirstOrDefaultAsync(m => m.Id.ToString() == id);

            if (existingMedia == null)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(updatedMedia.Title))
            {
                existingMedia.Title = updatedMedia.Title;
            }

            if (updatedMedia.DataFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await updatedMedia.DataFile.CopyToAsync(memoryStream);
                    existingMedia.DataFile = memoryStream.ToArray();
                }
            }
            await dbContext.SaveChangesAsync();
            return existingMedia;
        }


        public async Task DeleteMediaAsync(string id)
        {
            Media? mediaForDelete = await dbContext
                .MediaFiles
                .Where(m => m.IsDeleted == false)
                .FirstOrDefaultAsync(m => m.Id.ToString() == id && m.IsDeleted == false);

            if (mediaForDelete != null)
            {
                mediaForDelete.IsDeleted = true;

                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> IsUserOwnThedMedia(string userId, string mediaId)
        {
            var media = await dbContext.MediaFiles
                .FirstOrDefaultAsync(m => m.Id.ToString() == mediaId);

            if (media != null)
            {
                return media.UserId.ToString() == userId;
            }

            return false;
        }
    }
}





