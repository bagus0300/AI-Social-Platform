using AI_Social_Platform.Services.Data.Interfaces;
using AI_Social_Platform.Services.Data.Models.SocialFeature;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AI_Social_Platform.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SocialFeatureController : ControllerBase
    {
        private readonly IBaseSocialService baseSocialService;

        public SocialFeatureController(IBaseSocialService baseSocialService)
        {
            this.baseSocialService = baseSocialService;
        }

        [HttpGet("notification",Name = "latest notifications")]
        public async Task<IEnumerable<NotificationDto>> All()
        {
            try
            {
                var notifications = await baseSocialService.GetLatestNotificationsAsync();
                return notifications;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
