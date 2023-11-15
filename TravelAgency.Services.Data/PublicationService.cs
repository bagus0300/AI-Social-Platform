using AI_Social_Platform.Data;
using AI_Social_Platform.Data.Models.Publication;
using AI_Social_Platform.Services.Data.Interfaces;
using AI_Social_Platform.Services.Data.Models.PublicationDtos;
using Microsoft.EntityFrameworkCore;

namespace AI_Social_Platform.Services.Data;

public class PublicationService : IPublicationService
{
    private readonly ASPDbContext dbContext;

    public PublicationService(ASPDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<IEnumerable<PublicationDto>> GetPublicationsAsync()
    {
        var publications = await
            dbContext.Publications
                .Include(p => p.Comments)
                .Select(p => new PublicationDto()
                {
                    Id = p.Id,
                    Content = p.Content,
                    DateCreated = p.DateCreated.ToShortDateString(),
                    AuthorId = p.AuthorId,
                    Comments = p.Comments.Select(c => new CommentDto()
                    {
                        AuthorId = c.AuthorId,
                        Content = c.Content,
                        DateCreated = c.DateCreated.ToShortDateString(),
                        Id = c.Id,
                        PublicationId = c.PublicationId
                    }).ToList(),
                })
                .ToListAsync();

        return publications;
    }
    
    public async Task<PublicationDto> GetPublicationAsync(Guid id)
    {
        var publication = await dbContext.Publications
            .Where(p => p.Id == id)
            .Include(p => p.Comments)
            .FirstOrDefaultAsync();

        if (publication == null)
        {
            throw new Exception("Publication not found");
        }

        PublicationDto publicationDto = new()
        {
            Id = publication.Id,
            Content = publication.Content,
            DateCreated = publication.DateCreated.ToShortDateString(),
            AuthorId = publication.AuthorId,
            Comments = publication.Comments.Select(c => new CommentDto()
            {
                AuthorId = c.AuthorId,
                Content = c.Content,
                DateCreated = c.DateCreated.ToShortDateString(),
                Id = c.Id,
                PublicationId = c.PublicationId
            }).ToList(),
        };

        return publicationDto;

    }
    
    public async Task CreatePublicationAsync(PublicationFormDto dto)
    {
       Publication publication = new()
       {
           Content = dto.Content,
           AuthorId = dto.AuthorId
       };

       await dbContext.AddAsync(publication);
       await dbContext.SaveChangesAsync();
    }
    
    public async Task UpdatePublicationAsync(PublicationFormDto dto, Guid id)
    {
        var publication = await dbContext.Publications.FirstOrDefaultAsync(p => p.Id == id);

        if (publication == null)
        {
            throw new Exception("Publication not found");
        }

        publication.Content = dto.Content;
        await dbContext.SaveChangesAsync();
    }
    
    public async Task DeletePublicationAsync(Guid id)
    {
        var publication = await dbContext.Publications
            .Include(p => p.Comments)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (publication == null)
        {
            throw new Exception("Publication not found");
        }

        dbContext.Comments.RemoveRange(publication.Comments);
        dbContext.Publications.Remove(publication);
        await dbContext.SaveChangesAsync();
    }

    //Comment
    public async Task CreateCommentAsync(CommentFormDto dto)
    {
        var publication = await dbContext.Publications
            .Include(p => p.Comments)
            .FirstOrDefaultAsync(p => p.Id == dto.PublicationId);

        if (publication == null)
        {
            throw new Exception("Publication not found");
        }

        publication.Comments.Add(new Comment()
        {
            AuthorId = dto.AuthorId,
            Content = dto.Content,
            PublicationId = dto.PublicationId
        });

        await dbContext.Comments.AddAsync(publication.Comments.Last());
        await dbContext.SaveChangesAsync();
    }

    public Task UpdateCommentAsync(CommentFormDto dto, Guid id)
    {
        throw new NotImplementedException();
    }

    public Task DeleteCommentAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}