using AI_Social_Platform.Services.Data.Models.PublicationDtos;

namespace AI_Social_Platform.Services.Data.Models.SocialFeature
{
    public class SearchTopicDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public int PublicationsCount { get; set; }

        public int FollowersCount { get; set; }

        public PublicationDto[] Publications { get; set; } = null!;

        public Guid[] Followers { get; set; } = null!;
    }
}
