using DAL.Context;
using DAL.Exceptions;
using DAL.Interfaces;
using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    internal sealed class CategoryRepository : ICategoryRepository
    {
        private readonly StoreContext _context;
        public CategoryRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<Category> GetById(int categoryId)
        {
            var genre = await _context.Categories.FirstOrDefaultAsync(p => p.CategoryId == categoryId);

            return genre ?? throw new EntryNotFoundException($"Genre with CategoryId={categoryId} was not found");
        }

        public async Task<IList<Category>> GetAll()
        {
            return await _context.Categories.AsNoTracking().ToListAsync();
        }

        public async Task<Category> Add(Category entity)
        {
            var genre = await _context.Categories.AddAsync(entity);
            return genre.Entity;
        }

        public async Task Update(int categoryId, Category updatedEntity)
        {
            var foundCategory = await _context.Categories.FirstOrDefaultAsync(p => p.CategoryId == categoryId);

            if(foundCategory == null)
                throw new EntryNotFoundException($"Genre with CategoryId={categoryId} was not found");

            _context.Entry(foundCategory).CurrentValues.SetValues(updatedEntity.ToSqlUpdateParameters());
        }

        public async Task Delete(int categoryId)
        {
            await _context.Database.ExecuteSqlCommandAsync("DELETE FROM Categories WHERE CategoryId=@categoryId",
                new MySqlParameter("@categoryId", categoryId));
        }
    }
}
