using AI_Social_Platform.Services.Data.Interfaces;
using AI_Social_Platform.Services.Data.Models.PublicationDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static AI_Social_Platform.Common.NotificationMessagesConstants;

namespace AI_Social_Platform.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PublicationController : ControllerBase
{
    private readonly IPublicationService publicationService;

    public PublicationController(IPublicationService publicationService)
    {
        this.publicationService = publicationService;
    }

    [HttpGet(Name = "all")]
    public async Task<IEnumerable<PublicationDto>> All(int page)
    {
        try
        {
            var publications = await publicationService.GetPublicationsAsync(page);
            return publications;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [HttpPost(Name = "create")]
    public async Task<IActionResult> Create(PublicationFormDto dto)
    {
        try
        {
            await publicationService.CreatePublicationAsync(dto);
            return CreatedAtAction(nameof(Create), new { message = PublicationSuccessfullyCreated });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}", Name = "get")]
    public async Task<IActionResult> Get(Guid id)
    {
        try
        {
            var publication = await publicationService.GetPublicationAsync(id);
            return Ok(publication);
        }
        catch (NullReferenceException ex)
        {
            return NotFound(ex.Message);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}",Name = "delete")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await publicationService.DeletePublicationAsync(id);
            return Ok(PublicationSuccessfullyDeleted);
        }
        catch (NullReferenceException ex)
        {
            return NotFound(ex.Message);
        }
        catch (AccessViolationException ex)
        {
            return StatusCode(403,ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}", Name = "update")]
    public async Task<IActionResult> Update(PublicationFormDto dto, Guid id)
    {
        try
        {
            await publicationService.UpdatePublicationAsync(dto, id);
            return Ok(PublicationSuccessfullyEdited);
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

    [HttpGet("comment/{publicationId}", Name = "getComment")]
    public async Task<IEnumerable<CommentDto>> GetComments(Guid publicationId)
    {
        try
        {
            var comments = await publicationService.GetCommentsOnPublicationAsync(publicationId);
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
            await publicationService.CreateCommentAsync(dto);
            return CreatedAtAction(nameof(CreateComment), new { message = CommentSuccessfullyCreated});
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
            await publicationService.DeleteCommentAsync(commentId);
            return Ok(CommentSuccessfullyDeleted);
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
    public async Task<IActionResult> UpdateComment(CommentFormDto dto, Guid commentId)
    {
        try
        {
            await publicationService.UpdateCommentAsync(dto, commentId);
            return Ok(CommentSuccessfullyEdited);
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
            var likes = await publicationService.GetLikesOnPublicationAsync(publicationId);
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
            await publicationService.CreateLikesOnPublicationAsync(publicationId);
            return CreatedAtAction(nameof(CreateLike), new { message = LikeSuccessfullyCreated });
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
            await publicationService.DeleteLikeOnPublicationAsync(likeId);
            return Ok(LikeSuccessfullyDeletedFromPublication);
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
            var shares = await publicationService.GetSharesOnPublicationAsync(publicationId);
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
            await publicationService.CreateSharesOnPublicationAsync(publicationId);
            return CreatedAtAction(nameof(CreateShare), new { message = ShareSuccessfullyCreated });
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
            await publicationService.DeleteShareOnPublicationAsync(shareId);
            return Ok(ShareSuccessfullyDeletedFromPublication);
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