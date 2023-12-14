namespace AI_Social_Platform.Server.Controllers
{
    using AI_Social_Platform.Data.Models.Publication;
    using AI_Social_Platform.Extensions;
    using AI_Social_Platform.Services.Data.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TopicController : ControllerBase
    {
        private readonly ITopicService topicService;
        public TopicController(ITopicService topicService)
        {
                this.topicService = topicService;
        }

        [HttpPost("Follow")]
        public async Task<IActionResult> FollowTopic(string topicId)
        {
            var userId = HttpContext.User.GetUserId();
            var result = await topicService.FollowTopicAsync(userId, topicId);
            return Ok(result);
        }

        [HttpPost("Unfollow")]
        public async Task<IActionResult> UnfollowTopic(string topicId)
        {
            var userId = HttpContext.User.GetUserId();
            var action = await topicService.UnfollowTopicAsync(userId, topicId);

            return Ok(action);
        }

        [HttpGet("GetTopics")]
        public async Task<IActionResult> GetFollowedTopics()
        {
            var userId = HttpContext.User.GetUserId();

            var result = await topicService.GetFollowedTopicsAsync(userId);
            return Ok(result);
        }

        [HttpGet("topics/{topicId}/posts")]
        public async Task<IActionResult> GetPublicationsByTopicId(string topicId)
        {
            ICollection<Publication> publications = await topicService.GetPublicationsByTopicIdAsync(topicId);
            if (!publications.Any())
            {
                return Ok("No publications for this topic yet");
            }

            return Ok(publications);
        }

    }
}
