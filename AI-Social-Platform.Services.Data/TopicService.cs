namespace AI_Social_Platform.Services.Data
{
    using AI_Social_Platform.Data;
    using AI_Social_Platform.Data.Models;
    using AI_Social_Platform.Data.Models.Topic;
    using AI_Social_Platform.Services.Data.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TopicService : ITopicService
    {
        private readonly ASPDbContext dbContext;
        public TopicService(ASPDbContext dbContext)
        {
                this.dbContext = dbContext;
        }
        public async Task<string> FollowTopicAsync(string userId, string topicId)
        {
            ApplicationUser? user = await dbContext.Users
                .FirstOrDefaultAsync(u => u.Id.ToString() == userId);
            Topic? topic = await dbContext.Topics
                .FirstOrDefaultAsync(t => t.Id.ToString() == topicId);

            var recordExist = dbContext.UsersTopics
                        .Any(ut => ut.UserId.ToString().ToLower() == userId && ut.TopicId.ToString() == topicId.ToLower());
            if (user == null || topic == null)
            {
                return "User or topic not found";
            }
            if (!recordExist)
            {
                UserTopic ut = new UserTopic()
                {
                    User = user!,
                    TopicId = Guid.Parse(topicId)
                };

                user!.FollowedTopics.Add(ut);
                await dbContext.SaveChangesAsync();

                return $"Successfully follow topic: {topic.Title}";
            }

            else
            {
                return "You alredy follow this topic";
            }
            
        }
    }
}
