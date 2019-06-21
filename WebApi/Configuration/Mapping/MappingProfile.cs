using AutoMapper;
using BLL.DTO;
using WebApi.VIewDto;

namespace WebApi.Configuration.Mapping
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.Initialize(c =>
            {
                c.CreateMap<CommentDto, CommentViewDto>().ReverseMap();
                c.CreateMap<GenreDto, GenreViewDto>().ReverseMap();
                c.CreateMap<PlatformTypeDto, PlatformViewDto>().ReverseMap();
                c.CreateMap<PublisherDto, PublisherViewDto>().ReverseMap();
                c.CreateMap<GameViewDto, GameDto>().ReverseMap();
                c.CreateMap<SubGenreDto, SubGenreViewDto>().ReverseMap();
                c.CreateMap<GenreDto, GenreViewDto>().ReverseMap();
                c.CreateMap<OrderViewDto, OrderDto>().ReverseMap();
                c.CreateMap<CustomUser, UserViewDto>()
                    .ForMember(p => p.Email, opt => opt.MapFrom(p => p.Email))
                    .ForMember(p => p.UserName, opt => opt.MapFrom(p => p.UserName))
                    .ForMember(p => p.IsPublisher, opt => opt.MapFrom(p => p.IsPublisher));
            });
        }
    }
}
