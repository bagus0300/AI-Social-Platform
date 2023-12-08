namespace AI_Social_Platform.Server.Controllers
{
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

        [HttpPost]
        public async Task<IActionResult> FollowTopic(string topicId)
        {
            var userId = HttpContext.User.GetUserId();
            var result = await topicService.FollowTopicAsync(userId, topicId);
            return Ok(result);
        }
    }
}
