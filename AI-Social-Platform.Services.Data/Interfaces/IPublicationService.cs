using AI_Social_Platform.Services.Data.Models.PublicationDtos;

namespace AI_Social_Platform.Services.Data.Interfaces;

public interface IPublicationService
{
    public Task<IEnumerable<PublicationDto>> GetPublicationsAsync();
    public Task<PublicationDto> GetPublicationAsync(Guid id);
    public Task CreatePublicationAsync(PublicationFormDto dto);

    public Task UpdatePublicationAsync(PublicationFormDto dto, Guid id);

    public Task DeletePublicationAsync(Guid id);

    public Task<IEnumerable<CommentDto>> GetCommentsOnPublicationAsync(Guid publicationId);

    public Task CreateCommentAsync(CommentFormDto dto, Guid publicationId);

    public Task UpdateCommentAsync(CommentFormDto dto, Guid id);

    public Task DeleteCommentAsync(Guid id);

  
}

