namespace AI_Social_Platform.Data.Models.Publication
{
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.Comment;

    public class Comment : BaseSocialFeature
    {
        public Comment() : base()
        {
    
        }
        [Required]
        [MaxLength(CommentContentMaxLength)]
        public string Content { get; set; } = null!;
    }
}
