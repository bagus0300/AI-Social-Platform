namespace AI_Social_Platform.Data.Configurations
{
    using System.Reflection;
    using System.Security.Cryptography;
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

            builder
                .HasMany(u => u.UserSchools)
                .WithOne(us => us.User)
                .HasForeignKey(us => us.UserId)
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
                NormalizedEmail = "user@user.com",
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                StateId = 1,
                CountryId = 1,
                Gender = Gender.Man,
                Birthday = DateTime.Parse("2005/10/11"),
                Relationship = RelationshipStatus.InARelationship,
                ProfilePicture = GenerateRandomImage(1024),
                CoverPhoto = GenerateRandomImage(2048)

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
                NormalizedEmail = "ADMIN@ADMIN.com",
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                StateId = 1,
                CountryId = 1,
                Gender = Gender.Man,
                Birthday = DateTime.Parse("2007/11/05"),
                Relationship = RelationshipStatus.Single,
                ProfilePicture = GenerateRandomImage(1024),
                CoverPhoto = GenerateRandomImage(2048)

            };
            user.PasswordHash = hasher.HashPassword(user, "123456");

            users.Add(user);

            return users.ToArray();
        }


        private static byte[] GenerateRandomImage(int imageSize)
        {
            byte[] imageBytes = new byte[imageSize];

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(imageBytes);
            }

            return imageBytes;
        }

    }
}
