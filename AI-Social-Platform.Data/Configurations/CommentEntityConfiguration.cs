
using AI_Social_Platform.Data.Models.Publication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AI_Social_Platform.Data.Configurations
{
    public class CommentEntityConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasOne(x => x.Publication)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.PublicationId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
