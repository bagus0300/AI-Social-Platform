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
    public async Task<IndexPublicationDto> All(int page)
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

    [HttpGet("User/{userId}",Name = "user publications")]
    public async Task<IndexPublicationDto> UserPublications(int page, Guid userId)
    {
        try
        {
            var publications = await publicationService.GetUserPublicationsAsync(page, userId);
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
            var publication = await publicationService.CreatePublicationAsync(dto);
            return CreatedAtAction(nameof(Create), new
            {
                message = PublicationSuccessfullyCreated,
                publication
            });
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
            return NotFound(new { message = ex.Message });
        }
        catch(Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}",Name = "delete")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await publicationService.DeletePublicationAsync(id);
            return Ok(new {message = PublicationSuccessfullyDeleted });
        }
        catch (NullReferenceException ex)
        {
            return NotFound(new { message = ex.Message});
        }
        catch (AccessViolationException ex)
        {
            return StatusCode(403, new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}", Name = "update")]
    public async Task<IActionResult> Update(PublicationFormDto dto, Guid id)
    {
        try
        {
            await publicationService.UpdatePublicationAsync(dto, id);
            return Ok(new {message = PublicationSuccessfullyEdited });
        }
        catch (NullReferenceException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (AccessViolationException ex)
        {
            return StatusCode(403, new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}