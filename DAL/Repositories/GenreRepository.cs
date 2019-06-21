using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    internal sealed class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        public GenreRepository(IAppContext context) : base(context)
        {

        }

        public async Task<IList<GameGenre>> GamesWithGenreId(int id)
        {
            var result = await Db.FindAsync(id);
            return result.GameGenres;
        }
    }
}
