using DAL.Context;
using DAL.Exceptions;
using DAL.Interfaces;
using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    internal sealed class ManufacturerRepository : IManufacturerRepository
    {
        private readonly StoreContext _context;

        public ManufacturerRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<Manufacturer> GetById(int manufacturerId)
        {
            var manufacturer = await _context.Manufacturers.FirstOrDefaultAsync(p => p.ManufacturerId == manufacturerId);

            return manufacturer ?? throw new EntryNotFoundException($"Manufacturer with ManufacturerId={manufacturerId} was not found");
        }

        public async Task<IList<Manufacturer>> GetAll()
        {
            return await _context.Manufacturers.AsNoTracking().ToListAsync();
        }

        public async Task<Manufacturer> Add(Manufacturer manufacturer)
        {
            var createdManufacturer = await _context.Manufacturers.AddAsync(manufacturer);
            return createdManufacturer.Entity;
        }

        public async Task Update(int manufacturerId, Manufacturer updatedEntity)
        {
            var foundManufacturer  = await _context.Manufacturers.FirstOrDefaultAsync(p => p.ManufacturerId == manufacturerId);

            if (foundManufacturer == null)
                throw new EntryNotFoundException($"Manufacturer with ManufacturerId={manufacturerId} was not found");

            _context.Entry(foundManufacturer).CurrentValues.SetValues(updatedEntity.ToSqlUpdateParameters());
        }

        public async Task Delete(int manufacturerId)
        {
            await _context.Database.ExecuteSqlCommandAsync("DELETE FROM Manufacturers WHERE ManufacturerId=@manufacturerId",
                new MySqlParameter("@manufacturerId", manufacturerId));
        }

        public async Task<List<Manufacturer>> GetManufacturersByCategory(int categoryId)
        {
            return await _context.Manufacturers.AsNoTracking()
                .Where(p => p.Products.Select(x => x.CategoryId).Contains(categoryId)).ToListAsync();
        }
    }
}
