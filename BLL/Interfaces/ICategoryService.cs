using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICategoryService
    {
        Task<int> Create(Category entity);
        Task Edit(int categoryId, Category entity);
        Task Delete(int categoryId);
        Task<Category> GetById(int categoryId);
        Task<IList<Category>> GetAll();
    }
}
