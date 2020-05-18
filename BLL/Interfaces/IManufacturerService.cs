using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IManufacturerService
    {
        Task<int> Create(Manufacturer entity);
        Task Edit(int id, Manufacturer entity);
        Task Delete(int id);
        Task<Manufacturer> GetById(int id);
        Task<List<Manufacturer>> GetAll();
        Task<List<Manufacturer>> GetManufacturersByCategory(int categoryId);
    }
}
