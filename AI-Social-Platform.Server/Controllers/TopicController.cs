namespace AI_Social_Platform.Server.Controllers
{
    using AI_Social_Platform.Data.Models.Publication;
    using AI_Social_Platform.Extensions;
    using AI_Social_Platform.FormModels;
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

        [HttpPost("Create")]
        public async Task<IActionResult> CreateTopic(CreateTopicFormModel model)
        {
            var creatorId = HttpContext.User.GetUserId();
            try
            {
                await topicService.CreateTopicAsync(creatorId, model);
                return Ok( new { message = $"Successfully created topic: {model.Title}" });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Sorry! Something went wrong"});
            }
        }

        [HttpPost("Follow")]
        public async Task<IActionResult> FollowTopic(string topicId)
        {
            var userId = HttpContext.User.GetUserId();

            try
            {
                var result = await topicService.FollowTopicAsync(userId, topicId);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Something went wrong!"});
            }
        }

        [HttpPost("Unfollow")]
        public async Task<IActionResult> UnfollowTopic(string topicId)
        {
            var userId = HttpContext.User.GetUserId();
            try
            {
                var action = await topicService.UnfollowTopicAsync(userId, topicId);

                return Ok(new { message = action});
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Something went wrong!"});
            }
        }

        [HttpGet("GetFollowedTopics")]
        public async Task<IActionResult> GetFollowedTopics()
        {
            var userId = HttpContext.User.GetUserId();

            try
            {
                var topics = await topicService.GetFollowedTopicsAsync(userId);
                if (!topics.Any())
                {
                    return BadRequest(new { message = "You don't have followed topics!"});
                }
                return Ok(topics);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Something went wrong!"});
            }
            
        }

        [HttpGet("topics/{topicId}/posts")]
        public async Task<IActionResult> GetPublicationsByTopicId(string topicId)
        {
            try
            {
                ICollection<Publication> publications = await topicService.GetPublicationsByTopicIdAsync(topicId);
                if (!publications.Any())
                {
                    return BadRequest("No publications for this topic yet");
                }

                return Ok(publications);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllTopics(int page)
        {
            var result = await topicService.GetAllTopicsAsync(page);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTopic(string id)
        {
            try
            {
                var result = await topicService.DeleteTopicAsync(id);
                if (result)
                {
                    return Ok("Successfully deleted topic!");
                }
                return BadRequest("Something went wrong!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
