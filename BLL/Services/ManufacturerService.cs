using BLL.Interfaces;
using DAL.Interfaces;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ManufacturerService : IManufacturerService
    {
        private readonly IUnitOfWork _unit;

        public ManufacturerService(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task<int> Create(Manufacturer manufacturer)
        {
            var resultPublisher = await _unit.ManufacturerRepository.Add(manufacturer);
            await _unit.Commit();
            return resultPublisher.ManufacturerId;
        }

        public async Task Edit(int manufacturerId, Manufacturer manufacturer)
        {
            await _unit.ManufacturerRepository.Update(manufacturerId, manufacturer);
            await _unit.Commit();
        }

        public async Task Delete(int manufacturerId)
        {
            await _unit.ManufacturerRepository.Delete(manufacturerId);
            await _unit.Commit();
        }

        public async Task<Manufacturer> GetById(int manufacturerId)
        {
            return await _unit.ManufacturerRepository.GetById(manufacturerId);
        }

        public async Task<List<Manufacturer>> GetAll()
        {
            var result = await _unit.ManufacturerRepository.GetAll();
            return result.ToList();
        }

        public async Task<List<Manufacturer>> GetManufacturersByCategory(int categoryId)
        {
            await _unit.CategoryRepository.GetById(categoryId);

            return await _unit.ManufacturerRepository.GetManufacturersByCategory(categoryId);
        }
    }
}
