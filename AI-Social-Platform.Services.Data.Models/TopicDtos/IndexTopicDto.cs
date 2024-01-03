namespace AI_Social_Platform.Services.Data.Models.TopicDtos
{
    using AI_Social_Platform.Services.Data.Models.SocialFeature;

    public class IndexTopicDto
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int TotalTopics { get; set; }
        public int TopicsLeft { get; set; }

        public IEnumerable<TopicDto> Topics { get; set; } = null!;
    }
}
