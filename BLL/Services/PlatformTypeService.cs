using BLL.Interfaces;
using DAL.Exceptions;
using DAL.Interfaces;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class PlatformTypeService : IPlatformService
    {
        private readonly IUnitOfWork _unit;
        public PlatformTypeService(IUnitOfWork unit)
        {
            _unit = unit;
        }
        public async Task<int> Create(PlatformType platform)
        {
            var createdPlatform = await _unit.PlatformRepository.Add(platform);
            await _unit.Commit();
            return createdPlatform.PlatformTypeId;
        }

        public async Task Edit(int platformId, PlatformType platform)
        {
            await _unit.PlatformRepository.Update(platformId, platform);
            await _unit.Commit();
        }

        public async Task Delete(int id)
        {
            await _unit.PlatformRepository.Delete(id);
            await _unit.Commit();
        }

        public async Task<PlatformType> GetById(int id)
        {
            var result = await _unit.PlatformRepository.GetById(id);
            if (result == null)
                throw new EntryNotFoundException($"Entry with id = {id} does not found");
            return result;
        }

        public async Task<IList<PlatformType>> GetAll()
        {
            var result = await _unit.PlatformRepository.GetAll();
            return result.ToList();
        }
    }
}
