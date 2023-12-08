namespace AI_Social_Platform.Services.Data.Interfaces
{
    using System.Threading.Tasks;

    public interface ITopicService
    {
        Task<string> FollowTopicAsync(string userId, string topicId);
    }
}
