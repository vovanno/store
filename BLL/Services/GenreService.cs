using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Exceptions;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        public GenreService(IUnitOfWork unit, IMapper mapper)
        {
            _mapper = mapper;
            _unit = unit;
        }
        public async Task<int> Create(GenreDto genre)
        {
            if (genre == null)
                throw new ArgumentNullException(nameof(genre));
            var temp = _mapper.Map<Genre>(genre);
            var resultGenre = await _unit.GenreRepository.Add(temp);
            await _unit.Commit();
            return resultGenre.GenreId;
        }

        public async Task Edit(GenreDto genre)
        {
            if (genre.GenreId < 0)
                throw new ArgumentException("Id can not be less than 0.");
            if (genre == null)
                throw new ArgumentNullException(nameof(genre));
            try
            {
                await _unit.GenreRepository.Update(genre.GenreId, _mapper.Map<Genre>(genre));
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
            await _unit.GenreRepository.Delete(id);
            await _unit.Commit();
        }

        public async Task<GenreDto> GetById(int id)
        {
            if (id < 0)
                throw new ArgumentException("Id can not be less than 0");
            var result = await _unit.GenreRepository.GetById(id);
            if (result == null)
                throw new EntryNotFoundException($"Entry with id = {id} does not found");
            return _mapper.Map<GenreDto>(result);
        }

        public async Task<IEnumerable<GenreDto>> GetAll()
        {
            var result = await _unit.GenreRepository.GetAll();
            return _mapper.Map<IEnumerable<GenreDto>>(result);
        }

        public async Task<IList<GameDto>> GamesWithGenreId(int id)
        {
            if (id < 0)
                throw new ArgumentException("Id can not be less than 0");
            var result = await _unit.GenreRepository.GamesWithGenreId(id);
            return _mapper.Map<IList<GameDto>>(result);
        }
    }
}
