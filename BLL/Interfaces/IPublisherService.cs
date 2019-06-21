using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IPublisherService : IBaseService<PublisherDto>
    {
        Task<IList<GameDto>> GetGamesByPublisherId(int id);
    }
}
