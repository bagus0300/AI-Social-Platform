using AI_Social_Platform.Data;
using AI_Social_Platform.Data.Models;
using AI_Social_Platform.Data.Models.Enums;
using AI_Social_Platform.Services.Data.Interfaces;
using AI_Social_Platform.Services.Data.Models.SocialFeature;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using AutoMapper;

namespace AI_Social_Platform.Services.Data
{
    public class BaseSocialService : IBaseSocialService
    {
        private readonly ASPDbContext dbContext;
        private readonly HttpContext httpContext;
        private readonly IMapper mapper;

        public BaseSocialService(ASPDbContext dbContext, IHttpContextAccessor accessor, IMapper mapper)
        {
            this.dbContext = dbContext;
            httpContext = accessor.HttpContext!;
            this.mapper = mapper;
        }

        public async Task CreateNotificationAsync(Guid receivingUserId, Guid creatingUserId, NotificationType notificationType, Guid? returningId)
        {
            var user = await dbContext.Users
                .Where(u => u.Id == creatingUserId)
                .Select(u => u.FirstName + " " + u.LastName)
                .FirstOrDefaultAsync();
            
            var notification = new Notification()
            {
                ReceivingUserId = receivingUserId,
                CreatingUserId = creatingUserId,
                NotificationType = notificationType
            };

            switch (notificationType)
            {
                case NotificationType.Comment: 
                    notification.Content = $"{user} commented your publication"; 
                    notification.RedirectUrl = $"/Publication/{returningId}";
                    break;
                case NotificationType.Like: 
                    notification.Content = $"{user} liked your publication";
                    notification.RedirectUrl = $"/Publication/{returningId}";
                    break;
                case NotificationType.Follow:
                    notification.Content = $"{user} followed you";
                    notification.RedirectUrl = $"/User/{creatingUserId}";
                    break;
                case NotificationType.Share: 
                    notification.Content = $"{user} shared your publication";
                    notification.RedirectUrl = $"/Publication/{returningId}";
                    break;
            }

            await dbContext.Notifications.AddAsync(notification);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<NotificationDto>> GetLatestNotificationsAsync()
        {
            int notificationListSize = 20;

            return await mapper.ProjectTo<NotificationDto>
            (dbContext.Notifications
                .AsQueryable()
                .Where(n => n.ReceivingUserId == GetUserId())
                .OrderByDescending(n => n.DateCreated)
                .Take(notificationListSize))
                .ToArrayAsync();
        }

        private Guid GetUserId()
        {
            var userId = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!;

            if (userId == null)
            {
                throw new NullReferenceException();
            }

            return Guid.Parse(userId);
        }
    }
}
