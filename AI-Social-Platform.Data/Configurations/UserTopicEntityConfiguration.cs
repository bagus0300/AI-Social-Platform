namespace AI_Social_Platform.Data.Configurations
{
    using AI_Social_Platform.Data.Models.Topic;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserTopicEntityConfiguration : IEntityTypeConfiguration<UserTopic>
    {
        public void Configure(EntityTypeBuilder<UserTopic> builder)
        {
            builder
                .HasKey(ut => new { ut.UserId, ut.TopicId });
        }
    }
}
