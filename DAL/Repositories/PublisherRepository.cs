using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    internal sealed class PublisherRepository : GenericRepository<Publisher>, IPublisherRepository
    {
        public PublisherRepository(IAppContext context) : base(context)
        {

        }

        public async Task<IList<Game>> GetGamesByPublisherId(int id)
        {
            var result = await Db.FindAsync(id);
            return result.Games;
        }
    }
}
