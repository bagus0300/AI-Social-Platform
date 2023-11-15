using AI_Social_Platform.Services.Data.Interfaces;
using AI_Social_Platform.Services.Data.Models.PublicationDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AI_Social_Platform.Server.Controllers;


[ApiController]
[Route("api/[controller]")]
public class PublicationController : ControllerBase
{
    private readonly IPublicationService publicationService;

    public PublicationController(IPublicationService publicationService)
    {
        this.publicationService = publicationService;
    }

    [HttpGet(Name = "all")]
    public async Task<IEnumerable<PublicationDto>> All()
    {
        try
        {
            var publications = await publicationService.GetPublicationsAsync();
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
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        try
        {
            await publicationService.CreatePublicationAsync(dto);
            return Ok();
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
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete(Name = "delete")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await publicationService.DeletePublicationAsync(id);
            return Ok();
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
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpPost("comment", Name = "createComment")]
    public async Task<IActionResult> CreateComment(CommentFormDto dto)
    {
        try
        {
            await publicationService.CreateCommentAsync(dto);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("comment/{id}", Name = "deleteComment")]
    public async Task<IActionResult> DeleteComment(Guid id)
    {
        try
        {
            await publicationService.DeleteCommentAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpPut("comment/{id}", Name = "updateComment")]
    public async Task<IActionResult> UpdateComment(CommentFormDto dto, Guid id)
    {
        try
        {
            await publicationService.UpdateCommentAsync(dto, id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}