namespace AI_Social_Platform.Data.Configurations
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;
    using Models.Enums;

    public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder
                .HasData(this.GenerateIdentityUser());

            builder
                .Property(u => u.FirstName)
                .HasDefaultValue("Test");

            builder
                .Property(u => u.LastName)
                .HasDefaultValue("Test");

            builder
                .Property(u => u.IsActive)
                .HasDefaultValue(true);

            builder
                .HasOne(u => u.Country)
                .WithMany(c => c.UsersInThisCountry)
                .HasForeignKey(u => u.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(u => u.State)
                .WithMany(c => c.UsersInThisState)
                .HasForeignKey(u => u.StateId)
                .OnDelete(DeleteBehavior.Restrict);

        }

        private ApplicationUser[] GenerateIdentityUser()
        {
            ICollection<ApplicationUser> users = new HashSet<ApplicationUser>();

            ApplicationUser user;
            var hasher = new PasswordHasher<ApplicationUser>();


            user = new ApplicationUser()
            {
                Id = Guid.Parse("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"),
                FirstName = "Georgi",
                LastName = "Georgiev",
                PhoneNumber = "0888555666",
                UserName = "user@user.com",
                NormalizedUserName = "USER@USER.com",
                Email = "user@user.com",
                School = "Ivan Vazov",
                NormalizedEmail = "user@user.com",
                SecurityStamp = "9c4f02ae-4f84-4acb-93ff-dd995539f7c6",
                ConcurrencyStamp = "ef38cf8a-c85f-4c3f-8479-d2c13337d6e8",
                StateId = 1,
                CountryId = 1,
                Gender = Gender.Man,
                Birthday = DateTime.Parse("2005/10/11"),
                Relationship = RelationshipStatus.InARelationship,
                PasswordHash = "AQAAAAEAACcQAAAAEE9dCggpv0kqUOfry6xWPND/bQS5lniTm/Vh8O1sGvvgkbm32tvJP9tA2x9PdkmuDw==" // 123456

            };
            user.PasswordHash = hasher.HashPassword(user, "123456");

            users.Add(user);

            user = new ApplicationUser()
            {
                Id = Guid.Parse("949a14ed-2e82-4f5a-a684-a9c7e3ccb52e"),
                FirstName = "Ivan",
                LastName = "Ivanov",
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.com",
                Email = "admin@admin.com",
                School = "Vasil Levski",
                NormalizedEmail = "ADMIN@ADMIN.com",
                SecurityStamp = "cfb5501d-596e-4bd5-b3e0-763e303fe980",
                ConcurrencyStamp = "515a311c-e50d-4f5e-ae9d-ed9e6d2cc786",
                StateId = 1,
                CountryId = 1,
                Gender = Gender.Man,
                Birthday = DateTime.Parse("2007/11/05"),
                Relationship = RelationshipStatus.Single,
                PasswordHash = "AQAAAAEAACcQAAAAEFG6pao9IbmHJA2KjJf+B+JKa/G/b6wivq1ojnEw3ysrEq4UdqOpzbvghwrnn3NJGA==" //123456

            };
            user.PasswordHash = hasher.HashPassword(user, "123456");

            users.Add(user);

            
            return users.ToArray();
        }
    }
}
