using AI_Social_Platform.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AI_Social_Platform.Data.Configurations
{
    public class UserSchoolEntityConfiguration : IEntityTypeConfiguration<UserSchool>
    {
        public void Configure(EntityTypeBuilder<UserSchool> builder)
        {
            builder.HasKey(us => new { us.UserId, us.SchoolId });
        }
    }
}
