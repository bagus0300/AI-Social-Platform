namespace AI_Social_Platform.Services.Data.Models.PublicationDtos
{
    public class PublicationDto
    {
        public Guid Id { get; set; }

        public string Content { get; set; } = null!;

        public string DateCreated { get; set; } = null!;

        //Relations
        public Guid AuthorId { get; set; }
    }
}
