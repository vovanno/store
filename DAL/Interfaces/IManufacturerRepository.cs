using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IManufacturerRepository
    {
        Task<Manufacturer> GetById(int manufacturerId);
        Task<IList<Manufacturer>> GetAll();
        Task<Manufacturer> Add(Manufacturer entity);
        Task Update(int manufacturerId, Manufacturer updatedEntity);
        Task Delete(int manufacturerId);
        Task<List<Manufacturer>> GetManufacturersByCategory(int categoryId);
    }
}
