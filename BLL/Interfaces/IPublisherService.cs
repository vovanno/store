using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Dtos;
using Domain.Entities;

namespace BLL.Interfaces
{
    public interface IPublisherService
    {
        Task<IList<GameDto>> GetGamesByPublisherId(int id);
        Task<int> Create(Publisher entity);
        Task Edit(int id, Publisher entity);
        Task Delete(int id);
        Task<Publisher> GetById(int id);
        Task<List<Publisher>> GetAll();
    }
}
