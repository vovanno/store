using AutoMapper;
using BLL.DTO;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace BLL.Configuration
{
    public class Configuration
    {
        public static void RegisterDependencies(IServiceCollection services, string connection)
        {
            services.AddScoped<UserManager<IdentityUser>>();
            DAL.Configuration.Configuration.RegisterDependencies(services, connection);

            var mapConfig = new MapperConfiguration(c =>
            {
                c.AddProfile(new BllToDalMapping());
            });
            var mapper = mapConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }

    internal class BllToDalMapping : Profile
    {
        public BllToDalMapping()
        {
            CreateMap<Game, GameDto>()
                .ForMember(p => p.Genres, opt =>
                       opt.MapFrom(p => p.GameGenres.Select(n => n.Genre))).ReverseMap();
            CreateMap<GameGenre, GenreDto>()
                .ForMember(p => p.GenreId, opt => opt.MapFrom(p => p.GenreId))
                .ForMember(p => p.Name, opt => opt.MapFrom(p => p.Genre.Name));
            CreateMap<GameGenre, GameDto>()
                .ForMember(p => p.Name, opt => opt.MapFrom(p => p.Game.Name))
                .ForMember(p => p.Description, opt => opt.MapFrom(p => p.Game.Description));
            CreateMap<Genre.SubGenre, SubGenreDto>().ReverseMap();
            CreateMap<GenreDto, Genre>().ReverseMap();
            CreateMap<Comment, CommentDto>().ReverseMap();
            CreateMap<PlatformType, PlatformTypeDto>().ReverseMap();
            CreateMap<Publisher, PublisherDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<OrderGame, GameDto>()
                .ForMember(p => p.GameId, opt => opt.MapFrom(p => p.Game.GameId))
                .ForMember(p => p.AmountOfViews, opt => opt.MapFrom(p => p.Game.AmountOfViews))
                .ForMember(p => p.DateOfAdding, opt => opt.MapFrom(p => p.Game.DateOfAdding))
                .ForMember(p => p.Description, opt => opt.MapFrom(p => p.Game.Description))
                .ForMember(p => p.Genres, opt => opt.MapFrom(p => p.Game.GameGenres.Select(g => g.Genre)))
                .ForMember(p => p.Name, opt => opt.MapFrom(p => p.Game.Name))
                .ForMember(p => p.Price, opt => opt.MapFrom(p => p.Game.Price))
                .ForMember(p => p.AmountOfViews, opt => opt.MapFrom(p => p.Game.AmountOfViews))
                .ReverseMap();
        }
    }
}
