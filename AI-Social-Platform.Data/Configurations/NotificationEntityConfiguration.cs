using AI_Social_Platform.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AI_Social_Platform.Data.Configurations
{
    public class NotificationEntityConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder
                .HasOne(n => n.CreatingUser)
                .WithMany(u => u.CreatingNotifications)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(n => n.ReceivingUser)
                .WithMany(u => u.ReceivingNotifications)
                .OnDelete(DeleteBehavior.NoAction);
                
        }
    }
}
