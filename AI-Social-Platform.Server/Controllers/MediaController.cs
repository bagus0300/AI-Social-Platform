namespace AI_Social_Platform.Server.Controllers
{
    using AI_Social_Platform.Data.Models;
    using AI_Social_Platform.FormModels;
    using AI_Social_Platform.Services.Data.Interfaces;
    using AI_Social_Platform.Services.Data.Models.MediaDtos;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using static Extensions.ClaimsPrincipalExtensions;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MediaController : ControllerBase
    {
        private readonly IMediaService mediaService;
        private readonly UserManager<ApplicationUser> userManager;
        public MediaController(IMediaService mediaService, UserManager<ApplicationUser> userManager)
        {
            this.mediaService = mediaService;
            this.userManager = userManager;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Post(IFormFileCollection files, bool? isItPublication)
        {
            var filesToUpload = Request.Form.Files;

            if (filesToUpload.Count == 0)
            {
                return BadRequest("No files to upload");
            }

            try
            {
                var userId = HttpContext.User.GetUserId();

                await mediaService.UploadMediaAsync(filesToUpload, userId!, isItPublication);
                return Ok("Successfully upload media");

            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpGet("serve/{mediaId}")]
        public async Task<IActionResult> GetMedia(string mediaId)
        {
            Media media = await mediaService.GetMediaAsync(mediaId);

            if (media == null)
            {
                return NotFound();
            }

            return File(media.DataFile, "image/png");
        }

        [HttpGet("get/{userId}")]
        public async Task<IActionResult> GetAllMediaByUserId(string userId)
        {
            try
            {
                var userMediaFiles = await mediaService.GetAllMediaByUserIdAsync(userId);


                var mediaFilesWithUrls = userMediaFiles.Select(mediaFile => new MediaWithUrl
                {
                    FileId = mediaFile.Id,
                    FileName = mediaFile.Title,
                    Url = GetImageUrl(mediaFile.Id)
                }).ToList();

                return Ok(mediaFilesWithUrls);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("{publicationId}")]
        public async Task<IActionResult> GetAllMediaByPublicationId(string publicationId)
        {
            try
            {
                var userMediaFiles = await mediaService.GetAllMediaByPublicationIdAsync(publicationId);

                var mediaFilesWithUrls = userMediaFiles.Select(mediaFile => new MediaWithUrl
                {
                    FileId = mediaFile.Id,
                    FileName = mediaFile.Title,
                    Url = GetImageUrl(mediaFile.Id)
                }).ToList();

                return Ok(mediaFilesWithUrls);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        [HttpPut("edit/{id}")]
        public async Task<IActionResult> ReplaceMedia(string id, [FromForm] MediaFormModel updatedMedia)
        {
            var userId = HttpContext.User.GetUserId();

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

            var userId = HttpContext.User.GetUserId();

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
        private string GetImageUrl(Guid fileId)
        {
            var request = HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";

            return $"{baseUrl}/api/Media/serve/{fileId}";
        }
    }
}