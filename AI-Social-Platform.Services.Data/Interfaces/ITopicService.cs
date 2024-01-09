namespace AI_Social_Platform.Services.Data.Interfaces
{
    using AI_Social_Platform.Data.Models.Publication;
    using AI_Social_Platform.Data.Models.Topic;
    using AI_Social_Platform.FormModels;
    using AI_Social_Platform.Services.Data.Models.TopicDtos;
    using System.Threading.Tasks;

    public interface ITopicService
    {
        Task<string> FollowTopicAsync(string userId, string topicId);
        Task<string> UnfollowTopicAsync(string userId, string topicId);

        Task<ICollection<Topic>> GetFollowedTopicsAsync(string userId);
        Task<ICollection<Publication>> GetPublicationsByTopicIdAsync(string topicId);
        Task CreateTopicAsync(string creatorId, CreateTopicFormModel model);
        Task<IndexTopicDto> GetAllTopicsAsync(int pageNum);
        Task<bool> DeleteTopicAsync(string id);
    }
}
