using AI_Social_Platform.Services.Data.Models.PublicationDtos;

namespace AI_Social_Platform.Services.Data.Interfaces;

public interface IPublicationService
{
    public Task<IndexPublicationDto> GetPublicationsAsync(int pageNum);
    public Task<PublicationDto> GetPublicationAsync(Guid id);
    public Task CreatePublicationAsync(PublicationFormDto dto);

    public Task UpdatePublicationAsync(PublicationFormDto dto, Guid id);

    public Task DeletePublicationAsync(Guid id);
}

