using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> GetById(int categoryId);
        Task<IList<Category>> GetAll();
        Task<Category> Add(Category entity);
        Task Update(int genreId, Category updatedEntity);
        Task Delete(int genreId);
    }
}
