using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Exceptions;
using DAL.Interfaces;

namespace BLL.Services
{
    public class SubGenreService : ISubGenreService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        public SubGenreService(IUnitOfWork unit, IMapper mapper)
        {
            _mapper = mapper;
            _unit = unit;
        }
        public async Task<int> Create(SubGenreDto subGenre)
        {
            if (subGenre == null)
                throw new ArgumentNullException(nameof(subGenre));
            var temp = _mapper.Map<Genre.SubGenre>(subGenre);
            var resultGame = await _unit.SubGenreRepository.Add(temp);
            await _unit.Commit();
            return resultGame.SubGenreId;
        }

        public async Task Edit(SubGenreDto subGenre)
        {
            if (subGenre.SubGenreId < 0)
                throw new ArgumentException("Id can not be less than 0.");
            if (subGenre == null)
                throw new ArgumentNullException(nameof(subGenre));
            try
            {
                await _unit.SubGenreRepository.Update(subGenre.SubGenreId, _mapper.Map<Genre.SubGenre>(subGenre));
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
            await _unit.SubGenreRepository.Delete(id);
            await _unit.Commit();
        }

        public async Task<SubGenreDto> GetById(int id)
        {
            if (id < 0)
                throw new ArgumentException("Id can not be less than 0");
            var result = await _unit.SubGenreRepository.GetById(id);
            if (result == null)
                throw new EntryNotFoundException($"Entry with id = {id} does not found");
            return _mapper.Map<SubGenreDto>(result);
        }

        public async Task<IEnumerable<SubGenreDto>> GetAll()
        {
            var result = await _unit.SubGenreRepository.GetAll();
            return _mapper.Map<IEnumerable<SubGenreDto>>(result);
        }
    }
}
