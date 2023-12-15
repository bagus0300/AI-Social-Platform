namespace AI_Social_Platform.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class SchoolEntityConfiguration : IEntityTypeConfiguration<School>
    {
        public void Configure(EntityTypeBuilder<School> builder)
        {
            builder
                .HasData(this.GenerateSchools());

            builder
                .HasMany(s => s.UserInThisSchool)
                .WithOne(u => u.School)
                .HasForeignKey(s => s.SchoolId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private School[] GenerateSchools()
        {
            ICollection<School> schools = new HashSet<School>();

            School school = new School()
            {
                Id = 1,
                Name = "Ivan Vazov",
                StateId = 1
            };
            
            schools.Add(school);

            return schools.ToArray();
        }
    }
}
