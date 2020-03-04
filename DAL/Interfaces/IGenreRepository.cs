using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IGenreRepository
    {
        Task<IList<Game>> GamesWithGenreId(int genreId);
        Task AddGenresRange(int gameId, int[] genreIds);
        Task<Genre> GetById(int genreId);
        Task<IList<Genre>> GetAll();
        Task<Genre> Add(Genre entity);
        Task Update(int genreId, Genre updatedEntity);
        Task Delete(int genreId);
    }
}
