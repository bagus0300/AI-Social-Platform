﻿using System.Collections;
using AI_Social_Platform.Data.Models.Enums;
using AI_Social_Platform.Services.Data.Models.SocialFeature;

namespace AI_Social_Platform.Services.Data.Interfaces
{
    public interface IBaseSocialService
    {
        //Notifications
        public Task CreateNotificationAsync(Guid receivingUserId, 
            Guid creatingUserId,
            NotificationType notificationType,
            Guid? returningId);
        public Task<int> GetNotificationsCountAsync();
        public Task<IEnumerable<NotificationDto>> GetLatestNotificationsAsync();
        public Task ReadNotificationAsync(Guid notificationId);

        //Search
        public Task<IEnumerable> SearchAsync(string type, string query);

        //Comments
        public Task<IndexCommentDto> GetCommentsOnPublicationAsync(Guid publicationId, int page);
        public Task<CommentDto> CreateCommentAsync(CommentFormDto dto);
        public Task<CommentDto> UpdateCommentAsync(CommentEditDto dto, Guid id);
        public Task DeleteCommentAsync(Guid id);

        //Likes
        public Task<IEnumerable<LikeDto>> GetLikesOnPublicationAsync(Guid publicationId);
        public Task<LikeDto> CreateLikesOnPublicationAsync(Guid publicationId);
        public Task<LikeDto> DeleteLikeOnPublicationAsync(Guid likeId);

        //Shares
        public Task<IEnumerable<ShareDto>> GetSharesOnPublicationAsync(Guid publicationId);
        public Task CreateSharesOnPublicationAsync(Guid publicationId);
        public Task DeleteShareOnPublicationAsync(Guid shareId);

    }
}
