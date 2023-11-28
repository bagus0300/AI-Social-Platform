namespace AI_Social_Platform.Data.Configurations
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasData(this.GenerateIdentityUser());
            builder.Property(u => u.FirstName)
                .HasDefaultValue("Test");
            builder.Property(u => u.LastName)
                .HasDefaultValue("Test");
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
                UserName = "user@user.com",
                NormalizedUserName = "USER@USER.com",
                Email = "user@user.com",
                NormalizedEmail = "user@user.com",
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
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
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            user.PasswordHash = hasher.HashPassword(user, "123456");

            users.Add(user);

            return users.ToArray();
        }
    }
}
