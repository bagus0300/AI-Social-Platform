using System.ComponentModel.DataAnnotations;
using static AI_Social_Platform.Common.EntityValidationConstants.Comment;

namespace AI_Social_Platform.Services.Data.Models.PublicationDtos
{
    public class CommentFormDto
    {
        [Required]
        [StringLength(CommentContentMaxLength, MinimumLength = 1)]
        public string Content { get; set; } = null!;

        public Guid PublicationId { get; set; } 
    }
}
