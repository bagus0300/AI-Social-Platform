namespace AI_Social_Platform.Data.Models.Topic
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class UserTopic
    {
        [ForeignKey(nameof(UserId))]
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;

        [ForeignKey(nameof(Topic))]
        public Guid TopicId { get; set; }
        public Topic Topic { get; set; } = null!;
    }
}
