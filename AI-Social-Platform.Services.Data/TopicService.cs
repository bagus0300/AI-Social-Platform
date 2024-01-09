namespace AI_Social_Platform.Services.Data
{
    using AI_Social_Platform.Data;
    using AI_Social_Platform.Data.Models;
    using AI_Social_Platform.Data.Models.Publication;
    using AI_Social_Platform.Data.Models.Topic;
    using AI_Social_Platform.FormModels;
    using AI_Social_Platform.Services.Data.Interfaces;
    using AI_Social_Platform.Services.Data.Models.SocialFeature;
    using AI_Social_Platform.Services.Data.Models.TopicDtos;
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

        public async Task<bool> DeleteTopicAsync(string id)
        {
            var topic = await dbContext.Topics.FirstAsync(t => t.Id.ToString() == id);
            if (topic != null)
            {
                dbContext.Topics.Remove(topic);
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
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

        public async Task<IndexTopicDto> GetAllTopicsAsync(int pageNum)
        {
            int pageSize = 5;
            if (pageNum <= 0) pageNum = 1;
            int skip = (pageNum - 1) * pageSize;

            IQueryable<Topic> allTopics = dbContext.Topics
                .AsQueryable();

            var pagedTopics = await allTopics
                .OrderByDescending(t => t.DateCreate)
                .Skip(skip)
                .Take(pageSize)
                .Select(t => new TopicDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Creator = t.Creator.FirstName + " " + t.Creator.LastName,
                    DataCreated = t.DateCreate.ToString("d")
                })
                .ToListAsync();

            int totalTopics = await dbContext.Topics.CountAsync();
            int topicsLeft = totalTopics - (pageNum * pageSize) < 0 ? 0 : totalTopics - (pageNum * pageSize);

            var indexTopicDto = new IndexTopicDto
            {
                Topics = pagedTopics,
                CurrentPage = pageNum,
                TotalPages = (int)Math.Ceiling(totalTopics / (double)pageSize),
                TotalTopics = totalTopics,
                TopicsLeft = topicsLeft
            };

            return indexTopicDto;
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
