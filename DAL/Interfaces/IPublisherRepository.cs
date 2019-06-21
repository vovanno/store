using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IPublisherRepository : IBaseRepository<Publisher>
    {
        Task<IList<Game>> GetGamesByPublisherId(int id);
    }
}
