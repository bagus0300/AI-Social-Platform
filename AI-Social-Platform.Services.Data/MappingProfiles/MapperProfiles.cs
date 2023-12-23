using AI_Social_Platform.Common;
using AI_Social_Platform.Data.Models;
using AI_Social_Platform.Data.Models.Enums;
using AI_Social_Platform.Data.Models.Publication;
using AI_Social_Platform.Data.Models.Topic;
using AI_Social_Platform.Services.Data.Models.PublicationDtos;
using AI_Social_Platform.Services.Data.Models.SocialFeature;
using AI_Social_Platform.Services.Data.Models.UserDto;
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
            this.CreateMap<Notification, NotificationDto>().ReverseMap()
                .ForMember(n => n.NotificationType,
                opt =>
                {
                    opt.MapFrom(n => n.NotificationType.ToString());
                });

            CreateMap<SearchTopicDto, Topic>().ReverseMap()
                .ForMember(n => n.FollowersCount, opt => { opt.MapFrom(n => n.Followers.Count); })
                .ForMember(p => p.PublicationsCount, opt => { opt.MapFrom(p => p.Publications.Count); })
                .ForMember(t => t.Followers, opt =>
                {
                    opt.MapFrom(t => t.Followers.Select(f => f.UserId).ToArray());
                });

            this.CreateMap<UserDto, ApplicationUser>().ReverseMap();
            this.CreateMap<TopicDto, Topic>().ReverseMap();
        }

    }
}
