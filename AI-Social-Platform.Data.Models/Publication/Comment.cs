namespace AI_Social_Platform.Data.Models.Publication
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static Common.EntityValidationConstants.Comment;

    public class Comment
    {
        public Comment()
        {
            this.Id = Guid.NewGuid();
            this.DateCreated = DateTime.UtcNow;
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(CommentContentMaxLength)]
        public string Content { get; set; } = null!;

        [Required]
        public DateTime DateCreated { get; set; }

        //Relations
        [Required]
        [ForeignKey(nameof(Publication))]
        public Guid PublicationId { get; set; }
        public Publication Publication { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Author))]
        public Guid AuthorId { get; set; }
        public ApplicationUser Author { get; set; } = null!;
    }
}
