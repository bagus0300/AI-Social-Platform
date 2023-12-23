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
        int totalPublications = await dbContext.Publications.CountAsync();
        int publicationsLeft = totalPublications - (pageNum * pageSize) < 0 ? 0 : totalPublications - (pageNum * pageSize);

        var indexPublicationDto = new IndexPublicationDto
        {
            Publications = await mapper.ProjectTo<PublicationDto>
            (dbContext.Publications
                .AsQueryable()
                .OrderByDescending(p => p.LatestActivity)
                .Skip(skip)
                .Take(pageSize)
            ).ToArrayAsync(),
            CurrentPage = pageNum,
            TotalPages = (int)Math.Ceiling((double)totalPublications / pageSize),
            PublicationsLeft = publicationsLeft
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