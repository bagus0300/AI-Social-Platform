using AI_Social_Platform.Data.Models.Publication;
using AI_Social_Platform.Services.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AI_Social_Platform.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PublicationController : ControllerBase
{
    private readonly IPublicationService publicationService;

    public PublicationController(IPublicationService publicationService)
    {
        this.publicationService = publicationService;
    }

    //[HttpGet(Name = "GetPublication")]
    //public async Task<IEnumerable<Publication>> Get()
    //{
    //    if (Response.StatusCode != 200) throw new InvalidOperationException("Error");
    //    return BadRequest();
    //}
}