namespace AI_Social_Platform.Services.Data.Models.PublicationDtos
{
    public class PublicationDto
    {
        public Guid Id { get; set; }

        public string Content { get; set; } = null!;

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public DateTime? LastCommented { get; set; }

        public DateTime LatestActivity { get; set; }

        //Relations
        public Guid AuthorId { get; set; }
    }
}
