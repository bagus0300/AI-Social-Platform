using System.ComponentModel.DataAnnotations;
using static AI_Social_Platform.Common.EntityValidationConstants.Publication;

namespace AI_Social_Platform.Services.Data.Models.PublicationDtos
{
    public class PublicationFormDto
    {
        [Required]
        [StringLength(PublicationContentMaxLength, MinimumLength = 1)]
        public string Content { get; set; } = null!;
    }
}
