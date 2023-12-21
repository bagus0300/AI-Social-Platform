namespace AI_Social_Platform.Services.Data
{
    using AI_Social_Platform.Data;
    using AI_Social_Platform.Data.Models;
    using AI_Social_Platform.Data.Models.Publication;
    using AI_Social_Platform.Data.Models.Topic;
    using AI_Social_Platform.FormModels;
    using AI_Social_Platform.Services.Data.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class TopicService : ITopicService
    {
        private readonly ASPDbContext dbContext;
        public TopicService(ASPDbContext dbContext)
        {
                this.dbContext = dbContext;
        }

        public async Task CreateTopicAsync(string creatorId, CreateTopicFormModel model)
        {
            var creator = await dbContext.Users.FirstAsync(u => u.Id.ToString() == creatorId);

            Topic topic = new Topic()
            {
                Title = model.Title,
                Creator = creator,
                DateCreate = DateTime.UtcNow
            };

            await dbContext.Topics.AddAsync(topic);
            await dbContext.SaveChangesAsync();
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
                topic.Followers.Add(ut);
                await dbContext.SaveChangesAsync();

                return $"Successfully follow topic: {topic.Title}";
            }

            else
            {
                return "You alredy follow this topic";
            }
            
        }

        public async Task<ICollection<Topic>> GetFollowedTopicsAsync(string userId)
        {
            var userIdGuid = Guid.Parse(userId);
            ICollection<Topic> topics = await dbContext.UsersTopics
                .Where(ut => ut.UserId == userIdGuid)
                .Select(ut => ut.Topic)
                .OrderByDescending(t => t.Publications.Count())
                .ToArrayAsync();

            if (topics != null)
            {
                return topics;
            }

            return null;
        }

        public async Task<ICollection<Publication>> GetPublicationsByTopicIdAsync(string topicId)
        {
            var topicIdGuid = Guid.Parse(topicId);
            var publications = await dbContext.Publications
                .Where(p => p.TopicId == topicIdGuid)
                .ToListAsync();

            return publications;
        }

        public async Task<string> UnfollowTopicAsync(string userId, string topicId)
        {
            var userIdGuid = Guid.Parse (userId);
            var topicIdGuid = Guid.Parse (topicId);

            Topic? topic = await dbContext.Topics
                .FirstOrDefaultAsync(t => t.Id == topicIdGuid);

            var record = await dbContext.UsersTopics
                .FirstAsync(ut => ut.UserId == userIdGuid && ut.TopicId == topicIdGuid);

            if (record != null)
            {
                dbContext.UsersTopics.Remove(record);
                await dbContext.SaveChangesAsync();

                return $"Successfully unfollowed topic: {topic.Title}";
            }

            return "Topic not found";
        }
    }
}
