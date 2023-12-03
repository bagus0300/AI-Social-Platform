using AI_Social_Platform.Data;
using AI_Social_Platform.Data.Models;
using AI_Social_Platform.Data.Models.Publication;
using Microsoft.AspNetCore.Identity;

namespace Ai_Social_Platform.Tests
{
    public static class DatabaseSeeder
    {
        public static List<Publication> Publications;
        public static HashSet<Comment> Comments;
        public static List<ApplicationUser> ApplicationUsers;
        public static List<Media> MediaFiles;
        public static List<Like> Likes;
        public static List<Share> Shares;


        public static void SeedDatabase(ASPDbContext dbContext)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            ApplicationUsers = new List<ApplicationUser>()
            { 
                new()
                {
                Id = Guid.Parse("123400ce-d726-4fc8-83d9-d6b3ac1f591e"),
                FirstName = "Georgi",
                LastName = "Testov",
                UserName = "user@test.com",
                NormalizedUserName = "USER@TEST.com",
                Email = "user@test.com",
                NormalizedEmail = "user@test.com",
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                PasswordHash = hasher.HashPassword(null, "123456")
                },
                new()
                {
                    Id = Guid.Parse("123456ed-2e82-4f5a-a684-a9c7e3ccb52e"),
                    FirstName = "Ivan",
                    LastName = "Testov",
                    UserName = "admin@test.com",
                    NormalizedUserName = "ADMIN@test.com",
                    Email = "admin@test.com",
                    NormalizedEmail = "ADMIN@test.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    PasswordHash = hasher.HashPassword(null, "123456")
                }
            };

            Publications = new List<Publication>()
            {
                new()
                {
                    Id = Guid.Parse("d0b0b6a0-0b1e-4b9e-9b0a-0b9b9b9b9b9b"),
                    Content = "This is the first seeded publication Content from Ivan",
                    AuthorId = Guid.Parse("123456ed-2e82-4f5a-a684-a9c7e3ccb52e")
                },
                new()
                {
                    Id = Guid.Parse("a0a0a6a0-0b1e-4b9e-9b0a-0b9b9b9b9b9b"),
                    Content = "This is the second seeded publication Content from Georgi",
                    AuthorId = Guid.Parse("123400ce-d726-4fc8-83d9-d6b3ac1f591e"),
                }
            };

            Comments = new HashSet<Comment>()
            {
                new()
                {
                    Content = "This is the first seeded comment Content from Ivan",
                    UserId = Guid.Parse("123456ed-2e82-4f5a-a684-a9c7e3ccb52e"),
                    PublicationId = Guid.Parse("d0b0b6a0-0b1e-4b9e-9b0a-0b9b9b9b9b9b")
                },
                new()
                {
                    Content = "This is the second seeded comment Content from Ivan",
                    UserId = Guid.Parse("123456ed-2e82-4f5a-a684-a9c7e3ccb52e"),
                    PublicationId = Guid.Parse("a0a0a6a0-0b1e-4b9e-9b0a-0b9b9b9b9b9b")
                },
                new()
                {
                    Content = "This is the first seeded comment Content from Georgi",
                    UserId = Guid.Parse("123400ce-d726-4fc8-83d9-d6b3ac1f591e"),
                    PublicationId = Guid.Parse("d0b0b6a0-0b1e-4b9e-9b0a-0b9b9b9b9b9b")
                },
                new()
                {
                    Content = "This is the second seeded comment Content from Georgi",
                    UserId = Guid.Parse("123400ce-d726-4fc8-83d9-d6b3ac1f591e"),
                    PublicationId = Guid.Parse("a0a0a6a0-0b1e-4b9e-9b0a-0b9b9b9b9b9b")
                }
              
            };

            Likes = new List<Like>()
            {
                new()
                {
                    UserId = Guid.Parse("123400ce-d726-4fc8-83d9-d6b3ac1f591e"),
                    PublicationId = Guid.Parse("d0b0b6a0-0b1e-4b9e-9b0a-0b9b9b9b9b9b")
                },
                new()
                {
                    UserId = Guid.Parse("123400ce-d726-4fc8-83d9-d6b3ac1f591e"),
                    PublicationId = Guid.Parse("a0a0a6a0-0b1e-4b9e-9b0a-0b9b9b9b9b9b")
                },
                new()
                {
                    UserId = Guid.Parse("123456ed-2e82-4f5a-a684-a9c7e3ccb52e"),
                    PublicationId = Guid.Parse("a0a0a6a0-0b1e-4b9e-9b0a-0b9b9b9b9b9b")
                }
            };
            Shares = new List<Share>()
            {
                new()
                {
                    UserId = Guid.Parse("123400ce-d726-4fc8-83d9-d6b3ac1f591e"),
                    PublicationId = Guid.Parse("d0b0b6a0-0b1e-4b9e-9b0a-0b9b9b9b9b9b")
                },
                new()
                {
                    UserId = Guid.Parse("123400ce-d726-4fc8-83d9-d6b3ac1f591e"),
                    PublicationId = Guid.Parse("a0a0a6a0-0b1e-4b9e-9b0a-0b9b9b9b9b9b")
                },
                new()
                {
                    UserId = Guid.Parse("123456ed-2e82-4f5a-a684-a9c7e3ccb52e"),
                    PublicationId = Guid.Parse("a0a0a6a0-0b1e-4b9e-9b0a-0b9b9b9b9b9b")
                }
            };

            MediaFiles = new List<Media>();

            dbContext.Users.AddRange(ApplicationUsers);
            dbContext.Publications.AddRange(Publications);
            dbContext.Comments.AddRange(Comments);
            dbContext.Likes.AddRange(Likes);
            dbContext.Shares.AddRange(Shares);
            dbContext.MediaFiles.AddRange(MediaFiles);
            dbContext.SaveChanges();
        }
    }
}
