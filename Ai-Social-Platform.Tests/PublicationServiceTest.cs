using AI_Social_Platform.Data;
using AI_Social_Platform.Data.Models.Publication;
using AI_Social_Platform.Services.Data;
using AI_Social_Platform.Services.Data.Models.PublicationDtos;

using AutoMapper;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static AI_Social_Platform.Common.ExceptionMessages.PublicationExceptionMessages;

namespace Ai_Social_Platform.Tests
{
    [TestFixture]
    public class PublicationServiceTest
    {
        private DbContextOptions<ASPDbContext> options;
        private ASPDbContext dbContext;
        private PublicationService publicationService;
        private HttpContextAccessor httpContextAccessor;
        private IMapper mapper;

        [SetUp]
        public void Setup()
        {
            this.options = new DbContextOptionsBuilder<ASPDbContext>()
                .UseInMemoryDatabase("ASPInMemory" + Guid.NewGuid())
                .Options;

            dbContext = new ASPDbContext(options);

            this.dbContext.Database.EnsureCreated();

            DatabaseSeeder.SeedDatabase(dbContext);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "123400ce-d726-4fc8-83d9-d6b3ac1f591e")
            };

            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var user = new ClaimsPrincipal(identity);

            var httpContext = new DefaultHttpContext
            {
                User = user
            };

            httpContextAccessor = new HttpContextAccessor
            {
                HttpContext = httpContext
            };

            var config = new MapperConfiguration(x =>
            {
                x.CreateMap<LikeDto, Like>().ReverseMap();
                x.CreateMap<CommentDto, Comment>().ReverseMap();
                x.CreateMap<CommentFormDto, Comment>().ReverseMap();
                x.CreateMap<ShareDto, Share>().ReverseMap();
                x.CreateMap<PublicationDto, Publication>().ReverseMap();
                x.CreateMap<PublicationFormDto, Publication>().ReverseMap();
            });
            mapper = config.CreateMapper();
            publicationService = new PublicationService(dbContext, httpContextAccessor, mapper);
        }

        [TearDown]
        public void OneTimeTearDown()
        {
            dbContext.Dispose();
        }

        [Test]
        public async Task GetPublicationsAsync_ReturnsPublications()
        {
            // Act
            var result = await publicationService.GetPublicationsAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<PublicationDto>>(result);

            // Assuming some publications were seeded during setup
            Assert.IsTrue(result.Any());
        }

        [Test]
        public async Task GetPublicationAsync_ValidId_ReturnsPublicationDto()
        {
            // Arrange - Assuming there's a publication with a known ID in the seeded data
            var knownPublicationId = dbContext.Publications.First().Id;

            // Act
            var result = await publicationService.GetPublicationAsync(knownPublicationId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<PublicationDto>(result);
            Assert.That(result.Id, Is.EqualTo(knownPublicationId));
        }

        [Test]
        public async Task GetPublicationAsync_InvalidId_ThrowsNullReferenceException()
        {
            // Arrange - Assuming there's an invalid publication ID
            var invalidPublicationId = Guid.NewGuid(); // Use a non-existing ID

            // Act and Assert
            var exception = Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await publicationService.GetPublicationAsync(invalidPublicationId);
            });

            // Optionally assert on the exception message or other details
            Assert.That(exception!.Message, Is.EqualTo(PublicationNotFound));
        }

        [Test]
        public async Task GetPublicationAsync_ValidId_ReturnsPublicationDtoWithSameIdAndContent()
        {
            //Arrange - Assuming there's a publication with a known ID in the seeded data
            var knownPublication = dbContext.Publications.First();

            // Act
            var result = await publicationService.GetPublicationAsync(knownPublication.Id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<PublicationDto>(result);
            Assert.That(result.Id, Is.EqualTo(knownPublication.Id));
            Assert.That(result.Content, Is.EqualTo(knownPublication.Content));
            Assert.That(result.DateCreated, Is.EqualTo(knownPublication.DateCreated));
            Assert.That(result.AuthorId, Is.EqualTo(knownPublication.AuthorId));
        }

        [Test]
        public async Task CreatePublicationAsync_ValidDto_CreatesPublication()
        {
            // Arrange
            var dto = new PublicationFormDto()
            {
                Content = "This is a test publication"
            };
            var countBefore = dbContext.Publications.Count();

            // Act
            await publicationService.CreatePublicationAsync(dto);

            // Assert
            Assert.That(dbContext.Publications.Count(), Is.EqualTo(countBefore + 1));
        }

        [Test]
        public async Task CreatePublicationAsync_ValidDto_CreatesPublicationWithCorrectContent()
        {
            // Arrange
            var dto = new PublicationFormDto()
            {
                Content = "This is a test publication"
            };

            // Act
            await publicationService.CreatePublicationAsync(dto);

            // Assert
            Assert.That(dbContext.Publications.Last().Content, Is.EqualTo(dto.Content));
        }

        [Test]
        public async Task CreatePublicationAsync_ValidDto_CreatesPublicationWithCorrectAuthorId()
        {
            // Arrange
            var dto = new PublicationFormDto()
            {
                Content = "This is a test publication"
            };
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            // Act
            await publicationService.CreatePublicationAsync(dto);

            // Assert
            Assert.That(dbContext.Publications.Last().AuthorId.ToString(), Is.EqualTo(userId));
        }

        [Test]
        public async Task UpdatePublicationAsync_ValidDtoAndId_UpdatesPublication()
        {
            // Arrange
            var dto = new PublicationFormDto()
            {
                Content = "This is a test publication"
            };
            var publication =
                dbContext.Publications.First(p => p.AuthorId.ToString() == "123400ce-d726-4fc8-83d9-d6b3ac1f591e");

            // Act
            await publicationService.UpdatePublicationAsync(dto, publication.Id);

            // Assert
            Assert.That(publication.Content, Is.EqualTo(dto.Content));
        }

        [Test]
        public void UpdatePublicationAsync_ThrowsNullReferenceException()
        {
            // Arrange
            var dto = new PublicationFormDto()
            {
                Content = "This is a test publication"
            };
            var invalidPublicationId = Guid.NewGuid(); // Use a non-existing ID

            // Act and Assert
            var exception = Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await publicationService.UpdatePublicationAsync(dto, invalidPublicationId);
            });

            // Optionally assert on the exception message or other details
            Assert.That(exception!.Message, Is.EqualTo(PublicationNotFound));
        }


        [Test]
        public void UpdatePublicationAsync_ThrowsAccessViolationExceptionException()
        {
            // Arrange
            var dto = new PublicationFormDto()
            {
                Content = "This is a test publication"
            };
            var publication =
                dbContext.Publications.First(p => p.AuthorId.ToString() == "949a14ed-2e82-4f5a-a684-a9c7e3ccb52e");

            // Act and Assert
            var exception = Assert.ThrowsAsync<AccessViolationException>(async () =>
            {
                await publicationService.UpdatePublicationAsync(dto, publication.Id);
            });

            // Optionally assert on the exception message or other details
            Assert.That(exception!.Message, Is.EqualTo(NotAuthorizedToEditPublication));
        }

        [Test]
        public async Task DeletePublicationAsync_ValidId_DeletesPublication()
        {
            // Arrange
            var publicationId =
                dbContext.Publications.First(p => p.AuthorId.ToString() == "123400ce-d726-4fc8-83d9-d6b3ac1f591e").Id;
            var countBefore = dbContext.Publications.Count();

            // Act
            await publicationService.DeletePublicationAsync(publicationId);

            // Assert
            Assert.That(dbContext.Publications.Count(), Is.EqualTo(countBefore - 1));
        }

        [Test]
        public Task DeletePublicationAsync_ThrowsNullReferenceException()
        {
            // Arrange
            var invalidPublicationId = Guid.NewGuid(); // Use a non-existing ID

            // Act and Assert
            var exception = Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await publicationService.DeletePublicationAsync(invalidPublicationId);
            });

            // Optionally assert on the exception message or other details
            Assert.That(exception!.Message, Is.EqualTo(PublicationNotFound));
            return Task.CompletedTask;
        }

        [Test]
        public async Task DeletePublicationAsync_ThrowsAccessViolationExceptionException()
        {
            // Arrange
            var publication =
                dbContext.Publications.First(p => p.AuthorId.ToString() == "949a14ed-2e82-4f5a-a684-a9c7e3ccb52e");

            // Act and Assert
            var exception = Assert.ThrowsAsync<AccessViolationException>(async () =>
            {
                await publicationService.DeletePublicationAsync(publication.Id);
            });

            // Optionally assert on the exception message or other details
            Assert.That(exception!.Message, Is.EqualTo(NotAuthorizedToDeletePublication));
        }

        [Test]
        public async Task DeletePublicationAsync_DeletesCollections()
        {
            // Arrange
            var publicationId = dbContext.Publications.First(p => p.AuthorId.ToString() == "123400ce-d726-4fc8-83d9-d6b3ac1f591e").Id;

            // Act
            await publicationService.DeletePublicationAsync(publicationId);

            // Assert
            Assert.IsEmpty(dbContext.Comments.Where(c => c.PublicationId == publicationId));
            Assert.IsEmpty(dbContext.Likes.Where(l => l.PublicationId == publicationId));
            Assert.IsEmpty(dbContext.Shares.Where(s => s.PublicationId == publicationId));
            Assert.IsEmpty(dbContext.MediaFiles.Where(p => p.Id == publicationId));
        }

        [Test]
        public async Task CreateCommentAsync_ValidDto_CreatesComment()
        {
            // Arrange
            var dto = new CommentFormDto()
            {
                Content = "This is a test comment",
                PublicationId = Guid.Parse("a0a0a6a0-0b1e-4b9e-9b0a-0b9b9b9b9b9b")
            };
            var countBefore = dbContext.Comments.Count(c => c.PublicationId == dto.PublicationId);

            // Act
            await publicationService.CreateCommentAsync(dto);

            // Assert
            Assert.That(dbContext.Comments.Count(c => c.PublicationId == dto.PublicationId),
                Is.EqualTo(countBefore + 1));
        }

        [Test]
        public async Task CreateCommentAsync_ValidDto_CreatesCommentWithCorrectContent()
        {
            // Arrange
            var dto = new CommentFormDto()
            {
                Content = "This is a test comment",
                PublicationId = dbContext.Publications.First().Id
            };

            // Act
            await publicationService.CreateCommentAsync(dto);

            // Assert
            Assert.That(dbContext.Comments.Last().Content, Is.EqualTo(dto.Content));
        }

        [Test]
        public async Task CreateCommentAsync_InValidPublicationId_ThrowsNullReferenceException()
        {
            // Arrange
            var dto = new CommentFormDto()
            {
                Content = "This is a test comment"
            };
            var invalidPublicationId = Guid.NewGuid(); // Use a non-existing ID

            // Act and Assert
            var exception = Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await publicationService.CreateCommentAsync(dto);
            });

            // Optionally assert on the exception message or other details
            Assert.That(exception!.Message, Is.EqualTo(PublicationNotFound));
        }

        [Test]
        public async Task UpdateCommentAsync_ValidDtoAndId_UpdatesComment()
        {
            // Arrange
            var dto = new CommentFormDto()
            {
                Content = "This is a test comment"
            };
            var comment =
                dbContext.Comments.First(c => c.UserId.ToString() == "123400ce-d726-4fc8-83d9-d6b3ac1f591e");

            // Act
            await publicationService.UpdateCommentAsync(dto, comment.Id);

            // Assert
            Assert.That(comment.Content, Is.EqualTo(dto.Content));
        }

        [Test]
        public async Task UpdateCommentAsync_ThrowsNullReferenceException()
        {
            // Arrange
            var dto = new CommentFormDto()
            {
                Content = "This is a test comment"
            };
            var invalidCommentId = Guid.NewGuid(); // Use a non-existing ID

            // Act and Assert
            var exception = Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await publicationService.UpdateCommentAsync(dto, invalidCommentId);
            });

            // Optionally assert on the exception message or other details
            Assert.That(exception!.Message, Is.EqualTo(CommentNotFound));
        }

        [Test]
        public async Task UpdateCommentAsync_ThrowsAccessViolationExceptionException()
        {
            // Arrange
            var dto = new CommentFormDto()
            {
                Content = "This is a test comment"
            };
            var comment =
                dbContext.Comments.First(c => c.UserId.ToString() == "123456ed-2e82-4f5a-a684-a9c7e3ccb52e");

            // Act and Assert
            var exception = Assert.ThrowsAsync<AccessViolationException>(async () =>
            {
                await publicationService.UpdateCommentAsync(dto, comment.Id);
            });

            // Optionally assert on the exception message or other details
            Assert.That(exception!.Message, Is.EqualTo(NotAuthorizedToEditComment));
        }

        [Test]
        public async Task DeleteCommentAsync_ValidId_DeletesComment()
        {
            // Arrange
            var comment =
                dbContext.Comments.First(c => c.UserId.ToString() == "123400ce-d726-4fc8-83d9-d6b3ac1f591e");
            var countBefore = dbContext.Comments.Count();

            // Act
            await publicationService.DeleteCommentAsync(comment.Id);

            // Assert
            Assert.That(dbContext.Comments.Count(), Is.EqualTo(countBefore - 1));
        }

        [Test]
        public async Task DeleteCommentAsync_ThrowsNullReferenceException()
        {
            // Arrange
            var invalidCommentId = Guid.NewGuid(); // Use a non-existing ID

            // Act and Assert
            var exception = Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await publicationService.DeleteCommentAsync(invalidCommentId);
            });

            // Optionally assert on the exception message or other details
            Assert.That(exception!.Message, Is.EqualTo(CommentNotFound));
        }

        [Test]
        public async Task DeleteCommentAsync_ThrowsAccessViolationExceptionException()
        {
            // Arrange
            var comment =
                dbContext.Comments.First(c => c.UserId.ToString() == "123456ed-2e82-4f5a-a684-a9c7e3ccb52e");

            // Act and Assert
            var exception = Assert.ThrowsAsync<AccessViolationException>(async () =>
            {
                await publicationService.DeleteCommentAsync(comment.Id);
            });

            // Optionally assert on the exception message or other details
            Assert.That(exception!.Message, Is.EqualTo(NotAuthorizedToDeleteComment));
        }

        [Test]
        public async Task GetCommentsOnPublicationAsync_ValidPublicationId_ReturnsComments()
        {
            // Arrange
            var publicationId = Guid.Parse("a0a0a6a0-0b1e-4b9e-9b0a-0b9b9b9b9b9b");

            // Act
            var result = await publicationService.GetCommentsOnPublicationAsync(publicationId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<CommentDto>>(result);

            // Assuming some comments were seeded during setup
            Assert.IsTrue(result.Any());
        }

        //Like Tests
        [Test]
        public async Task LikePublicationAsync_ValidPublicationId_LikesPublication()
        {
            // Arrange
            var publicationId = Guid.Parse("a0a0a6a0-0b1e-4b9e-9b0a-0b9b9b9b9b9b");
            var countBefore = dbContext.Likes.Count(l => l.PublicationId == publicationId);

            // Act
            await publicationService.CreateLikesOnPublicationAsync(publicationId);

            // Assert
            Assert.That(dbContext.Likes.Count(l => l.PublicationId == publicationId), Is.EqualTo(countBefore + 1));
        }

        [Test]
        public async Task LikePublicationAsync_InvalidPublicationId_ThrowsNullReferenceException()
        {
            // Arrange
            var invalidPublicationId = Guid.NewGuid(); // Use a non-existing ID

            // Act and Assert
            var exception = Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await publicationService.CreateLikesOnPublicationAsync(invalidPublicationId);
            });

            // Optionally assert on the exception message or other details
            Assert.That(exception!.Message, Is.EqualTo(PublicationNotFound));
        }

        [Test]
        public async Task LikePublicationAsync_ValidPublicationIdAndUserId_LikesPublication()
        {
            // Arrange
            var publicationId = Guid.Parse("a0a0a6a0-0b1e-4b9e-9b0a-0b9b9b9b9b9b");
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var countBefore = dbContext.Likes.Count(l => l.PublicationId == publicationId);

            // Act
            await publicationService.CreateLikesOnPublicationAsync(publicationId);

            // Assert
            Assert.That(dbContext.Likes.Count(l => l.PublicationId == publicationId), Is.EqualTo(countBefore + 1));
            Assert.That(dbContext.Likes.Last().UserId.ToString(), Is.EqualTo(userId));
        }

        [Test]
        public async Task DeleteLikeOnPublicationAsync_ValidLikeId_DeletesLike()
        {
            // Arrange
            var likeId = dbContext.Likes.FirstOrDefault(l => l.UserId == Guid.Parse("123400ce-d726-4fc8-83d9-d6b3ac1f591e"))!.Id;
            var countBefore = dbContext.Likes.Count();

            // Act
            await publicationService.DeleteLikeOnPublicationAsync(likeId);

            // Assert
            Assert.That(dbContext.Likes.Count(), Is.EqualTo(countBefore - 1));
        }

        [Test]
        public async Task DeleteLikeOnPublicationAsync_ThrowsNullReferenceException()
        {
            // Arrange
            var invalidLikeId = Guid.NewGuid(); // Use a non-existing ID

            // Act and Assert
            var exception = Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await publicationService.DeleteLikeOnPublicationAsync(invalidLikeId);
            });

            // Optionally assert on the exception message or other details
            Assert.That(exception!.Message, Is.EqualTo(LikeNotFound));
        }

        [Test]
        public async Task DeleteLikeOnPublicationAsync_ThrowsAccessViolationExceptionException()
        {
            // Arrange
            var likeId = dbContext.Likes.FirstOrDefault(l => l.UserId == Guid.Parse("123456ed-2e82-4f5a-a684-a9c7e3ccb52e"))!.Id;

            // Act and Assert
            var exception = Assert.ThrowsAsync<AccessViolationException>(async () =>
            {
                await publicationService.DeleteLikeOnPublicationAsync(likeId);
            });

            // Optionally assert on the exception message or other details
            Assert.That(exception!.Message, Is.EqualTo(NotAuthorizedToDeleteLike));
        }

        [Test]
        public async Task GetLikesOnPublicationAsync_ValidPublicationId_ReturnsLikes()
        {
            // Arrange
            var publicationId = Guid.Parse("a0a0a6a0-0b1e-4b9e-9b0a-0b9b9b9b9b9b");

            // Act
            var result = await publicationService.GetLikesOnPublicationAsync(publicationId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<LikeDto>>(result);

            // Assuming some likes were seeded during setup
            Assert.IsTrue(result.Any());
        }

        //Share Tests
        [Test]
        public async Task SharePublicationAsync_ValidPublicationId_SharesPublication()
        {
            // Arrange
            var publicationId = Guid.Parse("a0a0a6a0-0b1e-4b9e-9b0a-0b9b9b9b9b9b");
            var countBefore = dbContext.Shares.Count(l => l.PublicationId == publicationId);

            // Act
            await publicationService.CreateSharesOnPublicationAsync(publicationId);

            // Assert
            Assert.That(dbContext.Shares.Count(l => l.PublicationId == publicationId), Is.EqualTo(countBefore + 1));
        }

        [Test]
        public async Task SharePublicationAsync_InvalidPublicationId_ThrowsNullReferenceException()
        {
            // Arrange
            var invalidPublicationId = Guid.NewGuid(); // Use a non-existing ID

            // Act and Assert
            var exception = Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await publicationService.CreateSharesOnPublicationAsync(invalidPublicationId);
            });

            // Optionally assert on the exception message or other details
            Assert.That(exception!.Message, Is.EqualTo(PublicationNotFound));
        }

        [Test]
        public async Task SharePublicationAsync_ValidPublicationIdAndUserId_SharesPublication()
        {
            // Arrange
            var publicationId = Guid.Parse("a0a0a6a0-0b1e-4b9e-9b0a-0b9b9b9b9b9b");
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var countBefore = dbContext.Shares.Count(l => l.PublicationId == publicationId);

            // Act
            await publicationService.CreateSharesOnPublicationAsync(publicationId);

            // Assert
            Assert.That(dbContext.Shares.Count(l => l.PublicationId == publicationId), Is.EqualTo(countBefore + 1));
            Assert.That(dbContext.Shares.Last().UserId.ToString(), Is.EqualTo(userId));
        }

        [Test]
        public async Task DeleteShareOnPublicationAsync_ValidShareId_DeletesShare()
        {
            // Arrange
            var shareId = dbContext.Shares.FirstOrDefault(l => l.UserId == Guid.Parse("123400ce-d726-4fc8-83d9-d6b3ac1f591e"))!.Id;
            var countBefore = dbContext.Shares.Count();

            // Act
            await publicationService.DeleteShareOnPublicationAsync(shareId);

            // Assert
            Assert.That(dbContext.Shares.Count(), Is.EqualTo(countBefore - 1));
        }

        [Test]
        public async Task DeleteShareOnPublicationAsync_ThrowsNullReferenceException()
        {
            // Arrange
            var invalidShareId = Guid.NewGuid(); // Use a non-existing ID

            // Act and Assert
            var exception = Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await publicationService.DeleteShareOnPublicationAsync(invalidShareId);
            });

            // Optionally assert on the exception message or other details
            Assert.That(exception!.Message, Is.EqualTo(ShareNotFound));
        }

        [Test]
        public async Task DeleteShareOnPublicationAsync_ThrowsAccessViolationExceptionException()
        {
            // Arrange
            var shareId = dbContext.Shares.FirstOrDefault(l => l.UserId == Guid.Parse("123456ed-2e82-4f5a-a684-a9c7e3ccb52e"))!.Id;

            // Act and Assert
            var exception = Assert.ThrowsAsync<AccessViolationException>(async () =>
            {
                await publicationService.DeleteShareOnPublicationAsync(shareId);
            });

            // Optionally assert on the exception message or other details
            Assert.That(exception!.Message, Is.EqualTo(NotAuthorizedToDeleteShare));
        }

        [Test]
        public async Task GetSharesOnPublicationAsync_ValidPublicationId_ReturnsShares()
        {
            // Arrange
            var publicationId = Guid.Parse("a0a0a6a0-0b1e-4b9e-9b0a-0b9b9b9b9b9b");

            // Act
            var result = await publicationService.GetSharesOnPublicationAsync(publicationId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<ShareDto>>(result);

            // Assuming some shares were seeded during setup
            Assert.IsTrue(result.Any());
            Assert.IsTrue(result.All(s => s.PublicationId == publicationId));
        }
}
}
