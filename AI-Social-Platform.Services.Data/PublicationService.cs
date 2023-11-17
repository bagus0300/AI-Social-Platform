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
                .OrderByDescending(p => p.DateCreated)
                .Select(p => new PublicationDto()
                {
                    Id = p.Id,
                    Content = p.Content,
                    DateCreated = p.DateCreated.ToShortDateString() 
                                  + " " 
                                  + p.DateCreated.ToShortTimeString(),
                    AuthorId = p.AuthorId
                })
                .ToListAsync();

        return publications;
    }
    
    public async Task<PublicationDto> GetPublicationAsync(Guid id)
    {
        var publication = await dbContext.Publications
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();

        if (publication == null)
        {
            throw new Exception("Publication not found");
        }

        PublicationDto publicationDto = new()
        {
            Id = publication.Id,
            Content = publication.Content,
            DateCreated = publication.DateCreated.ToShortDateString()
                          + " " 
                          + publication.DateCreated.ToShortTimeString(),
            AuthorId = publication.AuthorId
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

        if (publication.AuthorId != dto.AuthorId)
        {
            throw new Exception("You are not the author of this publication");
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
    public async Task CreateCommentAsync(CommentFormDto dto, Guid publicationId)
    {
        var publication = await dbContext.Publications
            .FirstOrDefaultAsync(p => p.Id == publicationId);

        if (publication == null)
        {
            throw new Exception("Publication not found");
        }

        await dbContext.Comments.AddAsync(new Comment()
        {
            AuthorId = dto.AuthorId,
            Content = dto.Content,
            PublicationId = publicationId
        });
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateCommentAsync(CommentFormDto dto, Guid id)
    {
       var comment = await dbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);

       if (comment == null)
       {
           throw new Exception("Comment not found");
       }

       if (comment.AuthorId != dto.AuthorId)
       {
           throw new Exception("You are not the author of this comment");
       }

       comment.Content = dto.Content;
       await dbContext.SaveChangesAsync();
    }

    public async Task DeleteCommentAsync(Guid id)
    {
        var comment = await dbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);

        if(comment == null)
        {
            throw new Exception("Comment not found");
        }

        dbContext.Comments.Remove(comment);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<CommentDto>> GetCommentsOnPublicationAsync(Guid publicationId)
    {
        var publicationComments = await dbContext.Comments
            .Where(c => c.PublicationId == publicationId)
            .OrderByDescending(c => c.DateCreated)
            .Select(c => new CommentDto()
            {
                Id = c.Id,
                Content = c.Content,
                DateCreated = c.DateCreated.ToShortDateString() 
                              + " " 
                              + c.DateCreated.ToShortTimeString(),
                AuthorId = c.AuthorId,
                PublicationId = c.PublicationId
            })
            .ToListAsync();

        return publicationComments;
    }
}