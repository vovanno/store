using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IGenreRepository : IBaseRepository<Genre>
    {
        Task<IList<GameGenre>> GamesWithGenreId(int id);
    }
}
