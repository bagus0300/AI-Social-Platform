namespace AI_Social_Platform.Server.Controllers
{
    using AI_Social_Platform.Data.Models;
    using AI_Social_Platform.FormModels;
    using AI_Social_Platform.Services.Data.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;    

    using static Extensions.ClaimsPrincipalExtensions;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MediaController : ControllerBase
    {
        private readonly IMediaService mediaService;
        public MediaController(IMediaService mediaService)
        {
            this.mediaService = mediaService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Post(IFormFile file)
        {
            if (file == null || file.Length <= 0)
            {
                return BadRequest("No file uploaded");
            }

            try
            {
                string userId = HttpContext.User.GetUserId()!;
                await mediaService.UploadMediaAsync(file, userId!);
                return Ok("Successfully upload media");

            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> ReplaceMedia(string id, [FromForm] MediaFormModel updatedMedia)
        {
            string userId = HttpContext.User.GetUserId()!;

            bool isUserOwner = await mediaService.IsUserOwnThedMedia(userId, id);

            if (!isUserOwner)
            {
                return BadRequest("You don't have permission over this file");
            }

            try
            {
                Media media = await mediaService.ReplaceOrEditMediaAsync(id, updatedMedia);

                if (media == null)
                {
                    return NotFound();
                }

                return Ok("Successfully edited!");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }

        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {

            if (id == null)
            {
                return BadRequest("Media is not selected");
            }

            string userId = HttpContext.User.GetUserId()!;

            bool isUserOwner = await mediaService.IsUserOwnThedMedia(userId, id);

            if (!isUserOwner)
            {
                return BadRequest("You don't have permission over this file");
            }

            try
            {
                await mediaService.DeleteMediaAsync(id);
                return Ok("The media was deleted succsessfully");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
        }
    }
}
