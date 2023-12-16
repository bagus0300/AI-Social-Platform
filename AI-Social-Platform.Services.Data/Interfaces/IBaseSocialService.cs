using System.Collections;
using AI_Social_Platform.Data.Models.Enums;
using AI_Social_Platform.Services.Data.Models.SocialFeature;

namespace AI_Social_Platform.Services.Data.Interfaces
{
    public interface IBaseSocialService
    {
        public Task CreateNotificationAsync(Guid receivingUserId, 
            Guid creatingUserId,
            NotificationType notificationType,
            Guid? returningId);

        public Task<IEnumerable<NotificationDto>> GetLatestNotificationsAsync();

        public Task<IEnumerable> SearchAsync(string type, string query);
    }
}
