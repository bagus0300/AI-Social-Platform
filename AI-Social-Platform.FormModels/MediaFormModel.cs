namespace AI_Social_Platform.FormModels
{
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.MediaConstants;

    public class MediaFormModel
    {
        public IFormFile DataFile { get; set; } = null!;

        [StringLength(MediaTitleMaxLength, MinimumLength = MediaTitleMinLength)]
        public string? Title { get; set; }
    }
}
