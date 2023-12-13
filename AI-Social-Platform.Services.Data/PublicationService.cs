using AI_Social_Platform.Data;
using AI_Social_Platform.Data.Models.Publication;
using AI_Social_Platform.Services.Data.Interfaces;
using AI_Social_Platform.Services.Data.Models.PublicationDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using AI_Social_Platform.Data.Models.Enums;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using static AI_Social_Platform.Common.ExceptionMessages.PublicationExceptionMessages;

namespace AI_Social_Platform.Services.Data;

public class PublicationService : IPublicationService
{
    private readonly ASPDbContext dbContext;
    private readonly HttpContext httpContext;
    private readonly IBaseSocialService baseSocialService;
    private readonly IMapper mapper;

    public PublicationService(ASPDbContext dbContext, 
        IHttpContextAccessor accessor,
        IMapper mapper,
        IBaseSocialService baseSocialService)
    {
        this.mapper = mapper;
        this.dbContext = dbContext;
        this.baseSocialService = baseSocialService;
        httpContext = accessor.HttpContext!;
    }
    public async Task<IEnumerable<PublicationDto>> GetPublicationsAsync(int pageNum)
    {
        int pageSize = 10;
        if (pageNum <= 0) pageNum = 1;
        int skip = (pageNum - 1) * pageSize;

        return await mapper.ProjectTo<PublicationDto>
            (dbContext.Publications
                .AsQueryable()
                .OrderByDescending(p => p.LatestActivity)
                .Skip(skip)
                .Take(pageSize)
            )
            .ToArrayAsync();
    }

    public async Task<PublicationDto> GetPublicationAsync(Guid id)
    {
        var publication = await dbContext.Publications
            .FirstOrDefaultAsync(p => p.Id == id);

        if (publication == null)
        {
            throw new NullReferenceException(PublicationNotFound);
        }
        return mapper.Map<PublicationDto>(publication);
    }
    
    public async Task CreatePublicationAsync(PublicationFormDto dto)
    {
        var userId = GetUserId();
        var publication = mapper.Map<Publication>(dto);
        publication.AuthorId = userId;

       await dbContext.AddAsync(publication);
       await dbContext.SaveChangesAsync();
    }
    
    public async Task UpdatePublicationAsync(PublicationFormDto dto, Guid id)
    {
        var publication = await dbContext.Publications.FirstOrDefaultAsync(p => p.Id == id);
        var userId = GetUserId();

        if (publication == null)
        {
            throw new NullReferenceException(PublicationNotFound);
        }

        if (publication.AuthorId != userId)
        {
            throw new AccessViolationException(NotAuthorizedToEditPublication);
        }

        publication.Content = dto.Content;
        publication.DateModified = DateTime.UtcNow;
        publication.LatestActivity = DateTime.UtcNow;
        await dbContext.SaveChangesAsync();
    }
    
    public async Task DeletePublicationAsync(Guid id)
    {
        var publication = await dbContext.Publications.FirstOrDefaultAsync(p => p.Id == id);

        var userId = GetUserId();

        if (publication == null)
        {
            throw new NullReferenceException(PublicationNotFound);
        }

        if (publication.AuthorId != userId)
        {
            throw new AccessViolationException(NotAuthorizedToDeletePublication);
        }

        dbContext.Publications.Remove(publication);
        await dbContext.SaveChangesAsync();
    }

    //Comment
    public async Task CreateCommentAsync(CommentFormDto dto)
    {
        var publication = await dbContext.Publications
            .FirstOrDefaultAsync(p => p.Id == dto.PublicationId);
        var userId = GetUserId();

        if (publication == null)
        {
            throw new NullReferenceException(PublicationNotFound);
        }
        
        var comment = mapper.Map<Comment>(dto);
        comment.UserId = userId;
        publication.LastCommented = DateTime.UtcNow;
        publication.LatestActivity = DateTime.UtcNow;

       await baseSocialService.CreateNotificationAsync(publication.AuthorId, userId, NotificationType.Comment,publication.Id);

        await dbContext.Comments.AddAsync(comment);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateCommentAsync(CommentFormDto dto, Guid id)
    {
       var comment = await dbContext.Comments.FindAsync(id);
       var userId = GetUserId();

       if (comment == null)
       {
           throw new NullReferenceException(CommentNotFound);
       }

       if (comment.UserId != userId)
       {
           throw new AccessViolationException(NotAuthorizedToEditComment);
       }
       comment.Content = dto.Content;
       await dbContext.SaveChangesAsync();
    }

    public async Task DeleteCommentAsync(Guid id)
    {
        var comment = await dbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);
        var userId = GetUserId();

        if (comment == null)
        {
            throw new NullReferenceException(CommentNotFound);
        }

        if (comment.UserId != userId)
        {
            throw new AccessViolationException(NotAuthorizedToDeleteComment);
        }
        dbContext.Comments.Remove(comment);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<CommentDto>> GetCommentsOnPublicationAsync(Guid publicationId)
    {
        return await dbContext.Comments
            .Where(c => c.PublicationId == publicationId)
            .ProjectTo<CommentDto>(mapper.ConfigurationProvider)
            .OrderByDescending(c => c.DateCreated)
            .ToListAsync();
    }

    //Like
    public async Task CreateLikesOnPublicationAsync(Guid publicationId)
    {
        var publication = await dbContext.Publications
            .FirstOrDefaultAsync(p => p.Id == publicationId);
        var userId = GetUserId();

        if (publication == null)
        {
            throw new NullReferenceException(PublicationNotFound);
        }

        if (await dbContext.Likes.AnyAsync(l => l.PublicationId == publicationId && l.UserId == userId))
        {
            throw new InvalidOperationException(AlreadyLiked);
        }

        var like = new Like
        {
            PublicationId = publicationId,
            UserId = userId
        };

        await baseSocialService
            .CreateNotificationAsync(
            publication.AuthorId, 
            userId, 
            NotificationType.Like,
            publication.Id);

        await dbContext.Likes.AddAsync(like);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteLikeOnPublicationAsync(Guid likeId)
    {
        var like = await dbContext.Likes.FirstOrDefaultAsync(l => l.Id == likeId);
        var userId = GetUserId();

        if (like == null)
        {
            throw new NullReferenceException(LikeNotFound);
        }

        if (like.UserId != userId)
        {
            throw new AccessViolationException(NotAuthorizedToDeleteLike);
        }

        dbContext.Likes.Remove(like);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<LikeDto>> GetLikesOnPublicationAsync(Guid publicationId)
    {
        return await dbContext.Likes
            .Where(l => l.PublicationId == publicationId)
            .ProjectTo<LikeDto>(mapper.ConfigurationProvider)
            .OrderByDescending(l => l.DateCreated)
            .ToListAsync();
    }

    //Share
    public async Task CreateSharesOnPublicationAsync(Guid publicationId)
    {
        var publication = await dbContext.Publications
            .FirstOrDefaultAsync(p => p.Id == publicationId);
        var userId = GetUserId();

        if (publication == null)
        {
            throw new NullReferenceException(PublicationNotFound);
        }

        var share = new Share
        {
            PublicationId = publicationId,
            UserId = userId
        };

        await baseSocialService.CreateNotificationAsync(
                       publication.AuthorId, 
                                  userId, 
                                  NotificationType.Share,
                                  publicationId);

        await dbContext.Shares.AddAsync(share);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteShareOnPublicationAsync(Guid shareId)
    {
        var share = await dbContext.Shares.FirstOrDefaultAsync(s => s.Id == shareId);
        var userId = GetUserId();

        if (share == null)
        {
            throw new NullReferenceException(ShareNotFound);
        }

        if (share.UserId != userId)
        {
            throw new AccessViolationException(NotAuthorizedToDeleteShare);
        }

        dbContext.Shares.Remove(share);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<ShareDto>> GetSharesOnPublicationAsync(Guid publicationId)
    {
        return await dbContext.Shares
            .Where(s => s.PublicationId == publicationId)
            .ProjectTo<ShareDto>(mapper.ConfigurationProvider)
            .OrderByDescending(s => s.DateCreated)
            .ToListAsync();
    }

    private Guid GetUserId()
    {
        var userId = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!;

        if (userId == null)
        {
            throw new NullReferenceException(PublicationAuthorNotFound);
        }

        return Guid.Parse(userId);
    }
}