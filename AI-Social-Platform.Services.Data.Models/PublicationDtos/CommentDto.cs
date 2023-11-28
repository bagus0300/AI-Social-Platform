namespace AI_Social_Platform.Services.Data.Models.PublicationDtos
{
    public class CommentDto
    {
        public Guid Id { get; set; }

        public string Content { get; set; } = null!;

        public DateTime DateCreated { get; set; }

        public Guid PublicationId { get; set; }

        public Guid AuthorId { get; set; }
    }
}
