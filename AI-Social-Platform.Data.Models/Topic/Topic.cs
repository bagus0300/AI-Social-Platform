﻿namespace AI_Social_Platform.Data.Models.Topic
{
    using Data.Models;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Topic
    {
        public Topic()
        {
            this.Id = Guid.NewGuid();
            this.Publications = new HashSet<Publication.Publication>();
            this.Followers = new HashSet<UserTopic>();
        }
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string? topicUrl { get; set; }

        public ICollection<Publication.Publication> Publications { get; set; }

        public ICollection<UserTopic> Followers { get; set; }

        public ApplicationUser Creator { get; set; }

        [ForeignKey(nameof(Creator))]
        public Guid CreatorId { get; set; }

        public DateTime DateCreate { get; set; }
    }
}
