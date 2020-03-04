using CrossCuttingFunctionality.FilterModels;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IGameRepository
    {
        Task<IList<Genre>> GetGameGenres(int gameId);
        Task LeaveComment(int gameId, Comment comment);
        Task<IList<Game>> GetGamesWithFilters(FilterModel filter);
        Task AddGenresRange(int gameId, int[] genreIds);
        Task<Game> GetById(int id);
        Task<IList<Game>> GetAll();
        Task<Game> Add(Game game, int[] genresIds, int[] platformsIds);
        Task Update(int id, Game updatedEntity);
        Task Delete(int id);
    }
}
