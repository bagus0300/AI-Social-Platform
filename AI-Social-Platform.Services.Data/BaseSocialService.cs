using AI_Social_Platform.Data;
using AI_Social_Platform.Data.Models;
using AI_Social_Platform.Data.Models.Enums;
using AI_Social_Platform.Services.Data.Interfaces;
using AI_Social_Platform.Services.Data.Models.SocialFeature;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using AutoMapper;
using System.Collections;
using AI_Social_Platform.Services.Data.Models.PublicationDtos;
using AI_Social_Platform.Services.Data.Models.UserDto;

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

        public async Task<IEnumerable> SearchAsync(string type, string query)
        {
            int take = 20;
            type = type.ToLower();

            var result = type switch
            {
                "user" => await SearchUsersAsync(query, take),
                "publication" => await SearchPublicationsAsync(query, take),
                "topic" => await SearchTopicsAsync(query, take),
                _ => throw new ArgumentException("Invalid search type"),
            };
            return result;
        }

        private async Task<IEnumerable> SearchUsersAsync(string query, int take)
        {
            return await mapper.ProjectTo<UserDto>
            (dbContext.Users
                           .AsQueryable()
                           .Where(u => u.FirstName.Contains(query) || u.LastName.Contains(query))
                           .Take(take))
                .ToArrayAsync();
        }

        private async Task<IEnumerable> SearchPublicationsAsync(string query, int take)
        {
            return await mapper.ProjectTo<PublicationDto>
                (dbContext.Publications
                .AsQueryable()
                .Where(p => p.Content.Contains(query))
                .OrderByDescending(p => p.LatestActivity)
                .Take(take))
                .ToArrayAsync();
        }

        private async Task<IEnumerable> SearchTopicsAsync(string query, int take)
        {
            return 
                await mapper.ProjectTo<SearchTopicDto>(dbContext.Topics
                        .Include(t => t.Followers)
                        .Include(t => t.Publications)
                        .AsQueryable()
                        .Where(t => t.Title.Contains(query))
                        .OrderByDescending(t => t.Followers.Count)
                        .Take(take))
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
