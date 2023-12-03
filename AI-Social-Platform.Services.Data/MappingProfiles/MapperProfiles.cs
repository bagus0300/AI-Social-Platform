using AI_Social_Platform.Data.Models.Publication;
using AI_Social_Platform.Services.Data.Models.PublicationDtos;
using AutoMapper;

namespace AI_Social_Platform.Services.Data.MappingProfiles
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            this.CreateMap<LikeDto, Like>().ReverseMap();
            this.CreateMap<CommentDto, Comment>().ReverseMap();
            this.CreateMap<CommentFormDto, Comment>().ReverseMap();
            this.CreateMap<ShareDto, Share>().ReverseMap();
            this.CreateMap<PublicationDto, Publication>().ReverseMap();
            this.CreateMap<PublicationFormDto, Publication>().ReverseMap();
            
        }

    }
}
