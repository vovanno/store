using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Dtos;

namespace DAL.Interfaces
{
    public interface IPublisherRepository
    {
        Task<IList<Game>> GetGamesByPublisherId(int id);
        Task<Publisher> GetById(int id);
        Task<IList<Publisher>> GetAll();
        Task<Publisher> Add(Publisher entity);
        Task Update(int id, Publisher updatedEntity);
        Task Delete(int id);
    }
}
