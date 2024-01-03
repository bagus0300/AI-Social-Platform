namespace AI_Social_Platform.Services.Data.Models.SocialFeature
{
    public class TopicDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;

        public string Creator { get; set; } = null;

        public string DataCreated { get; set; } 
    }
}
