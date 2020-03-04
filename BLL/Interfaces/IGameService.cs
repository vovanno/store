using CrossCuttingFunctionality.FilterModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Dtos;
using Domain.Entities;

namespace BLL.Interfaces
{
    public interface IGameService
    {
        Task<IList<Genre>> GetGameGenres(int gameId);
        Task LeaveComment(int id, Comment comment);
        Task<IList<Comment>> GetCommentsByGameId(int gameId);
        Task<IList<Game>> GetGamesWithFilters(FilterModel filter);
        Task AddGenresRange(int gameId, IList<Genre> genres);
        Task<int> Create(Game game, int[] genresIds, int[] platformsIds);
        Task Edit(int id, Game entity);
        Task Delete(int id);
        Task<Game> GetById(int id);
        Task<List<GameDto>> GetAll();
    }
}
