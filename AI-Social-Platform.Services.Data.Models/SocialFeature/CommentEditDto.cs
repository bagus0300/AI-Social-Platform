using System.ComponentModel.DataAnnotations;
using static AI_Social_Platform.Common.EntityValidationConstants.Comment;

namespace AI_Social_Platform.Services.Data.Models.SocialFeature
{
    public class CommentEditDto
    {
        [Required]
        [StringLength(CommentContentMaxLength, MinimumLength = 1)]
        public string Content { get; set; } = null!;
    }
}
