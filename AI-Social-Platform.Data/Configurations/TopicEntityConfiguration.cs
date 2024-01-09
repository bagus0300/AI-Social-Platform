namespace AI_Social_Platform.Data.Configurations
{
    using AI_Social_Platform.Data.Models.Topic;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class TopicEntityConfiguration : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
           
        }
    }
}
