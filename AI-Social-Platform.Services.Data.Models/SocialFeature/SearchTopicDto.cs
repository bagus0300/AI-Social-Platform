using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Social_Platform.Services.Data.Models.SocialFeature
{
    public class SearchTopicDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public int PublicationsCount { get; set; }

        public int FollowersCount { get; set; }
    }
}
