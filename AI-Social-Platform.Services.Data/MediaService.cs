﻿namespace AI_Social_Platform.Services.Data
{
    using AI_Social_Platform.Data;
    using AI_Social_Platform.Data.Models;
    using AI_Social_Platform.FormModels;
    using AI_Social_Platform.Services.Data.Interfaces;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;

    public class MediaService : IMediaService
    {
        private readonly ASPDbContext dbContext;
        public MediaService(ASPDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task UploadMediaAsync(IFormFileCollection files, string userId, bool? isItPublication)
        {
            var publicationId = Guid.Empty;
            if (isItPublication == true)
            {
                publicationId = await dbContext.Publications
                    .Where(p => p.AuthorId.ToString() == userId)
                    .OrderByDescending(p => p.DateCreated)
                    .Select(p => p.Id)
                    .FirstOrDefaultAsync();
            }
            
            foreach (var file in files)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);

                    byte[] fileBytes = memoryStream.ToArray();

                    Media fileToUpload = new Media()
                    {
                        UserId = Guid.Parse(userId),
                        DataFile = fileBytes,
                        PublicationId = publicationId == Guid.Empty ? null : publicationId,
                    };

                    await dbContext.MediaFiles.AddAsync(fileToUpload);
                }
            }
            await dbContext.SaveChangesAsync();
        }

        public async Task<Media> ReplaceOrEditMediaAsync(string id, MediaFormModel updatedMedia)
        {
            Media? existingMedia = await dbContext
               .MediaFiles
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
                .FirstOrDefaultAsync(m => m.Id.ToString() == id);

            if (mediaForDelete != null)
            {
                dbContext.MediaFiles.Remove(mediaForDelete);
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

        public async Task<Media> GetMediaAsync(string mediaId)
        {
            Media? mediaToReturn = await dbContext.MediaFiles
                .FirstOrDefaultAsync(m => m.Id.ToString() == mediaId);

            if (mediaToReturn == null)
            {
                return null;
            }

            return mediaToReturn;
        }

        public async Task<ICollection<Media>> GetAllMediaFilesByUserIdAsync(string userId)
        {
            List<Media> mediaFiles = await dbContext.MediaFiles
                .Where(m => m.UserId.ToString() == userId)
                .ToListAsync();

            return mediaFiles;
        }

        public async Task<ICollection<Media>> GetAllMediaByPublicationIdAsync(string publicationId)
        {
            var publicationIdGuid = Guid.Parse(publicationId);
            List<Media> mediaFiles = await dbContext.MediaFiles
                .Where(m => m.PublicationId == publicationIdGuid)
                .ToListAsync();

            return mediaFiles;
        }

        public async Task<ICollection<Media>> GetAllMediaByUserIdAsync(string userId)
        {
            var userIdGuid = Guid.Parse(userId);
            var mediaFiles = await dbContext.MediaFiles
                .Where(m => m.UserId == userIdGuid)
                .ToArrayAsync();

            return mediaFiles;
        }
    }
}