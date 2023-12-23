using AI_Social_Platform.Services.Data;
using AI_Social_Platform.Services.Data.Interfaces;
using AI_Social_Platform.Services.Data.Models.SocialFeature;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static AI_Social_Platform.Common.NotificationMessagesConstants;

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

        [HttpGet("search", Name = "search")]
        public async Task<IActionResult> Search(string type, string query)
        {
            try
            {
                var result = await baseSocialService.SearchAsync(type, query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("comment/{publicationId}", Name = "getComment")]
        public async Task<IndexCommentDto> GetComments(Guid publicationId, int page)
        {
            try
            {
                var comments = await baseSocialService.GetCommentsOnPublicationAsync(publicationId, page);
                return comments;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("comment", Name = "createComment")]
        public async Task<IActionResult> CreateComment(CommentFormDto dto)
        {
            try
            {
                var comment = await baseSocialService.CreateCommentAsync(dto);
                return CreatedAtAction(nameof(CreateComment), new
                {
                    Message = CommentSuccessfullyCreated,
                    Comment = comment
                });
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("comment/{commentId}", Name = "deleteComment")]
        public async Task<IActionResult> DeleteComment(Guid commentId)
        {
            try
            {
                await baseSocialService.DeleteCommentAsync(commentId);
                return Ok(new {Message = CommentSuccessfullyDeleted });
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
            catch (AccessViolationException ex)
            {
                return StatusCode(403, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("comment/{commentId}", Name = "updateComment")]
        public async Task<IActionResult> UpdateComment(CommentEditDto dto, Guid commentId)
        {
            try
            {
                await baseSocialService.UpdateCommentAsync(dto, commentId);
                return Ok(new {Message = CommentSuccessfullyEdited });
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
            catch (AccessViolationException ex)
            {
                return StatusCode(403, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Likes
        [HttpGet("like/{publicationId}", Name = "getLikes")]
        public async Task<IEnumerable<LikeDto>> GetLikes(Guid publicationId)
        {
            try
            {
                var likes = await baseSocialService.GetLikesOnPublicationAsync(publicationId);
                return likes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("like/{publicationId}", Name = "createLike")]
        public async Task<IActionResult> CreateLike(Guid publicationId)
        {
            try
            {
                await baseSocialService.CreateLikesOnPublicationAsync(publicationId);
                return CreatedAtAction(nameof(CreateLike), new { Message = LikeSuccessfullyCreated });
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("like/{likeId}", Name = "deleteLike")]
        public async Task<IActionResult> DeleteLike(Guid likeId)
        {
            try
            {
                await baseSocialService.DeleteLikeOnPublicationAsync(likeId);
                return Ok(new {Message = LikeSuccessfullyDeletedFromPublication });
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
            catch (AccessViolationException ex)
            {
                return StatusCode(403, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Shares
        [HttpGet("share/{publicationId}", Name = "getShares")]
        public async Task<IEnumerable<ShareDto>> GetShares(Guid publicationId)
        {
            try
            {
                var shares = await baseSocialService.GetSharesOnPublicationAsync(publicationId);
                return shares;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("share/{publicationId}", Name = "createShare")]
        public async Task<IActionResult> CreateShare(Guid publicationId)
        {
            try
            {
                await baseSocialService.CreateSharesOnPublicationAsync(publicationId);
                return CreatedAtAction(nameof(CreateShare), new { Message = ShareSuccessfullyCreated });
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("share/{shareId}", Name = "deleteShare")]
        public async Task<IActionResult> DeleteShare(Guid shareId)
        {
            try
            {
                await baseSocialService.DeleteShareOnPublicationAsync(shareId);
                return Ok(new {Message = ShareSuccessfullyDeletedFromPublication });
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
            catch (AccessViolationException ex)
            {
                return StatusCode(403, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

