using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IBaseService<TEntity>
    {
        Task<int> Create(TEntity entity);
        Task Edit(TEntity entity);
        Task Delete(int id);
        Task<TEntity> GetById(int id);
        Task<IEnumerable<TEntity>> GetAll();
    }
}
