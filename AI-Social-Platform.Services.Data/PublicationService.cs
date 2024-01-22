using AI_Social_Platform.Data;
using AI_Social_Platform.Data.Models.Publication;
using AI_Social_Platform.Services.Data.Interfaces;
using AI_Social_Platform.Services.Data.Models.PublicationDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using AI_Social_Platform.Services.Data.Models.SocialFeature;
using AI_Social_Platform.Services.Data.Models.UserDto;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using static AI_Social_Platform.Common.ExceptionMessages.PublicationExceptionMessages;

namespace AI_Social_Platform.Services.Data;

public class PublicationService : IPublicationService
{
    private readonly ASPDbContext dbContext;
    private readonly HttpContext httpContext;
    private readonly IMapper mapper;

    public PublicationService(ASPDbContext dbContext, 
        IHttpContextAccessor accessor,
        IMapper mapper)
    {
        this.mapper = mapper;
        this.dbContext = dbContext;
        httpContext = accessor.HttpContext!;
    }

    public async Task<IndexPublicationDto> GetPublicationsAsync(int pageNum)
    {
        int pageSize = 10;
        if (pageNum <= 0) pageNum = 1;
        int skip = (pageNum - 1) * pageSize;

        var userId = GetUserId();

        var userFriends =  dbContext.Friendships
            .Where(u => u.UserId == userId)
            .Select(f => f.FriendId);

        var publications = dbContext.Publications
            .Where(p => userFriends.Any(u => u == p.AuthorId))
            .OrderByDescending(p => p.LatestActivity)
            .Skip(skip)
            .Take(pageSize);

        var likeIds = await dbContext.Likes
            .Where(l => l.UserId == userId && publications.Select(p => p.Id)
                .Any(p => p == l.PublicationId)).ToListAsync();

        int totalPublications = await dbContext.Publications
            .Where(p => userFriends.Any(u => u == p.AuthorId)).CountAsync();

        int publicationsLeft = totalPublications - (pageNum * pageSize) < 0 ? 0 : totalPublications - (pageNum * pageSize);

        var publicationsDto = await publications
            .ProjectTo<PublicationDto>(mapper.ConfigurationProvider)
            .ToListAsync();

        publicationsDto.ForEach(p =>
        {
            p.IsLiked = likeIds.Any(l => l.PublicationId == p.Id);
        });

        var indexPublicationDto = new IndexPublicationDto
        {
            Publications = publicationsDto.OrderByDescending(p => p.LatestActivity),
            CurrentPage = pageNum,
            TotalPages = (int)Math.Ceiling(totalPublications / (double)pageSize),
            TotalPublications = totalPublications,
            PublicationsLeft = publicationsLeft
        };
        return indexPublicationDto;
    }

    public async Task<IndexPublicationDto> GetUserPublicationsAsync(int pageNum, Guid userId)
    {
        int pageSize = 10;
        if (pageNum <= 0) pageNum = 1;
        int skip = (pageNum - 1) * pageSize;

        var userPublicationsQuery = dbContext.Publications
            .Where(p => p.AuthorId == userId)
            .OrderByDescending(p => p.LatestActivity)
            .ProjectTo<PublicationDto>(mapper.ConfigurationProvider);

        var publicationsCount = userPublicationsQuery.Count();

        var publications = await userPublicationsQuery
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync();

        publications.ForEach(p => p.IsLiked = dbContext.Likes.Any(l => l.PublicationId == p.Id && l.UserId == GetUserId()));

        var indexPublicationDto = new IndexPublicationDto
        {
            Publications = publications,
            CurrentPage = pageNum,
            TotalPages = (int)Math.Ceiling(publicationsCount / (double)pageSize),
            TotalPublications = publicationsCount,
            PublicationsLeft = publicationsCount - (pageNum * 10) < 0 ? 0 : publicationsCount - (pageNum * 10)
        };
        return indexPublicationDto;
    }

    public async Task<PublicationDto> GetPublicationAsync(Guid id)
    {
        var publication = await dbContext.Publications
            .FirstOrDefaultAsync(p => p.Id == id);

        if (publication == null)
        {
            throw new NullReferenceException(PublicationNotFound);
        }
        var publicationDto = mapper.Map<PublicationDto>(publication);

        publicationDto.Author = mapper.Map<UserDto>
            (await dbContext.Users.FirstOrDefaultAsync(u => u.Id == publication.AuthorId));

        publicationDto.Topic = mapper.Map<TopicDto>
            (await dbContext.Topics.FirstOrDefaultAsync(t => t.Id == publication.TopicId));

        publicationDto.Comments = await dbContext.Comments
            .Where(c => c.PublicationId == publication.Id)
            .OrderByDescending(c => c.DateCreated)
            .Take(5)
            .ProjectTo<CommentDto>(mapper.ConfigurationProvider)
            .ToListAsync();

        publicationDto.CommentsCount = await dbContext.Comments
            .Where(c => c.PublicationId == publication.Id)
            .CountAsync();

       

        publicationDto.LikesCount = await dbContext.Likes.Where(l => l.PublicationId == publication.Id).CountAsync();
        publicationDto.IsLiked = await dbContext.Likes.AnyAsync(l => l.PublicationId == publication.Id && l.UserId == GetUserId());

        return publicationDto;
    }
    
    public async Task<PublicationDto> CreatePublicationAsync(PublicationFormDto dto)
    {
        var userId = GetUserId();
        var publication = mapper.Map<Publication>(dto);
        publication.AuthorId = userId;

       await dbContext.AddAsync(publication);
       await dbContext.SaveChangesAsync();

       var publicationDto = mapper.Map<PublicationDto>(publication);
       publicationDto.Author = mapper.Map<UserDto>(await dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId));
       publicationDto.Topic = mapper.Map<TopicDto>(await dbContext.Topics.FirstOrDefaultAsync(t => t.Id == publication.TopicId));

       return publicationDto;
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
        publication.TopicId = dto.TopicId;
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