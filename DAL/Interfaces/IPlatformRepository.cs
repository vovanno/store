using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace DAL.Interfaces
{
    public interface IPlatformRepository
    {
        Task<PlatformType> GetById(int id);
        Task<IList<PlatformType>> GetAll();
        Task<PlatformType> Add(PlatformType entity);
        Task Update(int id, PlatformType updatedEntity);
        Task Delete(int id);
    }
}
