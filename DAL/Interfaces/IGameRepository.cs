using CrossCuttingFunctionality.FilterModels;
using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IGameRepository : IBaseRepository<Game>
    {
        Task<IList<GameGenre>> GetGameGenres(int gameId);
        Task LeaveComment(int gameId, Comment comment);
        Task<IList<Comment>> GetCommentsByGameId(int gameId);
        Task<IList<Game>> GetGamesWithFilters(FilterModel filter);
        Task AddGenresRange(int gameId, IList<Genre> genres);
    }
}
