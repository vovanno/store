using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    internal class SubGenresRepository : GenericRepository<Genre.SubGenre>, ISubGenreRepository
    {
        public SubGenresRepository(IAppContext context) : base(context)
        {

        }
    }
}
