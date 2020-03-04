using System.Collections.Generic;
using Domain.Entities;
using OnlineStoreApi.AuthApi;
using OnlineStoreApi.CommentApi;
using OnlineStoreApi.GameApi;
using OnlineStoreApi.GenresApi;
using OnlineStoreApi.PlatformApi;
using OnlineStoreApi.PublisherApi;
using OnlineStoreApi.SubGenreApi;
using System.Linq;
using Domain;
using Domain.Dtos;

namespace WebApi
{
    public static class Mappings
    {
        public static CommentResponse ToCommentResponse(this Comment comment)
        {
            return new CommentResponse
            {
                GameId = comment.GameId,
                CommentId = comment.CommentId,
                Name = comment.Name,
                DateOfAdding = comment.DateOfAdding,
                AmountOfLikes = comment.AmountOfLikes,
                Body = comment.Body,
                ParentId = comment.ParentCommentId
            };
        }

        public static List<CommentResponse> ToCommentResponse(this IList<Comment> comments)
        {
            return comments.EmptyIfNull().Select(p => new CommentResponse
            {
                Name = p.Name,
                AmountOfLikes = p.AmountOfLikes,
                Body = p.Body,
                DateOfAdding = p.DateOfAdding,
                CommentId = p.CommentId,
                ParentId = p.CommentId,
                GameId = p.GameId
            }).ToList();
        }

        public static Comment ToCommentModel(this CreateCommentRequest request)
        {
            return new Comment
            {
                Body = request.Body,
                GameId = request.GameId,
                ParentCommentId = request.ParentId
            };
        }

        public static Comment ToCommentModel(this EditCommentRequest request)
        {
            return new Comment
            {
                Body = request.Body
            };
        }

        public static UserProfile ToUserProfile(this RegisterUserRequest request)
        {
            return new UserProfile
            {
                Email = request.Email,
                UserName = request.UserName
            };
        }

        public static UserProfile ToUserProfile(this LoginUserRequest request)
        {
            return new UserProfile
            {
                Email = request.Email,
                UserName = request.UserName
            };
        }

        public static GameResponse ToGameResponse(this Game game)
        {
            return new GameResponse
            {
                GameId = game.GameId,
                DateOfAdding = game.DateOfAdding,
                Name = game.Name,
                AmountOfViews = game.AmountOfViews,
                Price = game.Price,
                Description = game.Description,
                Genres = game.GameGenres == null ? null : game.GameGenres.Select(p => p.Genre.ToGenreResponse()).ToList()
            };
        }

        public static GameResponse ToGameResponse(this GameDto game)
        {
            return new GameResponse
            {
                Name = game.Name,
                GameId = game.GameId,
                DateOfAdding = game.DateOfAdding,
                Description = game.Description,
                Price = game.Price,
                AmountOfViews = game.AmountOfViews,
                Publisher = game.Publisher.ToPublisherResponse(),
                Genres = game.GameGenres.ToGenreResponse(),
                Comments = game.Comments.ToCommentResponse()
            };
        }

        public static PublisherResponse ToPublisherResponse(this Publisher publisher)
        {
            return new PublisherResponse
            {
                Name = publisher.Name,
                PublisherId = publisher.PublisherId
            };
        }

        public static Game ToGameModel(this CreateGameRequest request)
        {
            return new Game
            {
                Description = request.Description,
                Name = request.Name,
                //GenreIds = request.GenresIds,
                //PlatformIds = request.PlatformsIds,
                PublisherId = request.PublisherId,
                Price = request.Price
            };
        }

        public static Game ToGameModel(this EditGameRequest request)
        {
            return new Game
            {
                Description = request.Description,
                Name = request.Name,
                //GenreIds = request.GenresIds,
                //PlatformIds = request.PlatformsIds,
                PublisherId = request.PublisherId,
                Price = request.Price
            };
        }

        public static GenreResponse ToGenreResponse(this Genre genre)
        {
            return new GenreResponse
            {
                Name = genre.Name,
                GenreId = genre.GenreId
            };
        }

        public static List<GenreResponse> ToGenreResponse(this IList<Genre> gameGenre)
        {
            return gameGenre.EmptyIfNull().Select(p => new GenreResponse
            {
                Name = p.Name,
                GenreId = p.GenreId
            }).ToList();
        }

        public static Genre ToGenreModel(this CreateGenreRequest request)
        {
            return new Genre
            {
                Name = request.Name
            };
        }

        public static Genre ToGenreModel(this EditGenreRequest request)
        {
            return new Genre
            {
                Name = request.Name
            };
        }

        public static Publisher ToPublisherModel(this CreatePublisherRequest request)
        {
            return new Publisher
            {
                Name = request.Name
            };
        }

        public static Publisher ToPublisherModel(this EditPublisherRequest request)
        {
            return new Publisher
            {
                Name = request.Name
            };
        }

        public static SubGenreResponse ToSubGenreResponse(this Genre.SubGenre subGenre)
        {
            return new SubGenreResponse
            {
                Name = subGenre.Name,
                Id = subGenre.SubGenreId,
                GenreId = subGenre.GenreId
            };
        }

        public static Genre.SubGenre ToSubGenre(this CreateSubGenreRequest request)
        {
            return new Genre.SubGenre
            {
                Name = request.Name,
                GenreId = request.GenreId
            };
        }
        public static Genre.SubGenre ToSubGenre(this EditSubGenreRequest request)
        {
            return new Genre.SubGenre
            {
                Name = request.Name,
                GenreId = request.GenreId
            };
        }

        public static PlatformType ToPlatformTypeModel(this CreatePlatformRequest request)
        {
            return new PlatformType
            {
                Type = request.Name
            };
        }

        public static PlatformType ToPlatformTypeModel(this EditPlatformRequest request)
        {
            return new PlatformType
            {
                Type = request.Name
            };
        }

        public static PlatformResponse ToPlatformResponse(this PlatformType platform)
        {
            return new PlatformResponse
            {
                PlatformTypeId = platform.PlatformTypeId,
                Type = platform.Type
            };
        }
    }
}
