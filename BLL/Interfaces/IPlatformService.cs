using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace BLL.Interfaces
{
    public interface IPlatformService
    {
        Task<int> Create(PlatformType entity);
        Task Edit(int id, PlatformType entity);
        Task Delete(int id);
        Task<PlatformType> GetById(int id);
        Task<IList<PlatformType>> GetAll();
    }
}
