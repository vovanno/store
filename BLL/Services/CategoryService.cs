using BLL.Interfaces;
using DAL.Interfaces;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unit;

        public CategoryService(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task<int> Create(Category category)
        {
            var createdEntity = await _unit.CategoryRepository.Add(category);
            await _unit.Commit();
            return createdEntity.CategoryId;
        }

        public async Task Edit(int categoryId, Category category)
        {
            await _unit.CategoryRepository.Update(categoryId, category);
            await _unit.Commit();
        }

        public async Task Delete(int categoryId)
        {
            await _unit.CategoryRepository.Delete(categoryId);
            await _unit.Commit();
        }

        public async Task<Category> GetById(int categoryId)
        {
            var result = await _unit.CategoryRepository.GetById(categoryId);
            return result;
        }

        public async Task<IList<Category>> GetAll()
        {
            var result = await _unit.CategoryRepository.GetAll();
            return result.ToList();
        }
    }
}
