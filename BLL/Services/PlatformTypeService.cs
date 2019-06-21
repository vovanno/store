using AutoMapper;
using BLL.DTO;
using DAL.Entities;
using DAL.Exceptions;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Interfaces;

namespace BLL.Services
{
    public class PlatformTypeService : IPlatformService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        public PlatformTypeService(IUnitOfWork unit, IMapper mapper)
        {
            _mapper = mapper;
            _unit = unit;
        }
        public async Task<int> Create(PlatformTypeDto platform)
        {
            if (platform == null)
                throw new ArgumentNullException(nameof(platform));
            var temp = _mapper.Map<PlatformType>(platform);
            var resultPlatformType = await _unit.PlatformRepository.Add(temp);
            await _unit.Commit();
            return resultPlatformType.PlatformTypeId;
        }

        public async Task Edit(PlatformTypeDto platform)
        {
            if (platform.PlatformTypeId < 0)
                throw new ArgumentException("Id can not be less than 0.");
            if (platform == null)
                throw new ArgumentNullException(nameof(platform));
            try
            {
                await _unit.PlatformRepository.Update(platform.PlatformTypeId, _mapper.Map<PlatformType>(platform));
            }
            catch (EntryNotFoundException e)
            {
                throw new ArgumentException("Entry does not found.", e);
            }
            await _unit.Commit();
        }

        public async Task Delete(int id)
        {
            if (id < 0)
                throw new ArgumentException("Id can not be less than 0");
            await _unit.PlatformRepository.Delete(id);
            await _unit.Commit();
        }

        public async Task<PlatformTypeDto> GetById(int id)
        {
            if (id < 0)
                throw new ArgumentException("Id can not be less than 0");
            var result = await _unit.PlatformRepository.GetById(id);
            if (result == null)
                throw new EntryNotFoundException($"Entry with id = {id} does not found");
            return _mapper.Map<PlatformTypeDto>(result);
        }

        public async Task<IEnumerable<PlatformTypeDto>> GetAll()
        {
            var result = await _unit.PlatformRepository.GetAll();
            return _mapper.Map<IEnumerable<PlatformTypeDto>>(result);
        }
    }
}
