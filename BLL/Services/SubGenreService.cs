using BLL.Interfaces;
using DAL.Exceptions;
using DAL.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class SubGenreService : ISubGenreService
    {
        private readonly IUnitOfWork _unit;
        public SubGenreService(IUnitOfWork unit)
        {
            _unit = unit;
        }
        public async Task<int> Create(Genre.SubGenre subGenre)
        {
            var temp = subGenre;
            var resultGameId = await _unit.SubGenreRepository.Add(temp);
            await _unit.Commit();
            return resultGameId;
        }

        public async Task Edit(int subGenreId, Genre.SubGenre subGenre)
        {
            await _unit.SubGenreRepository.Update(subGenreId, subGenre);
            await _unit.Commit();
        }

        public async Task Delete(int id)
        {
            if (id < 0)
                throw new ArgumentException("Id can not be less than 0");
            await _unit.SubGenreRepository.Delete(id);
            await _unit.Commit();
        }

        public async Task<Genre.SubGenre> GetById(int id)
        {
            if (id < 0)
                throw new ArgumentException("Id can not be less than 0");
            var result = await _unit.SubGenreRepository.GetById(id);
            if (result == null)
                throw new EntryNotFoundException($"Entry with id = {id} does not found");
            return result;
        }

        public async Task<List<Genre.SubGenre>> GetAll()
        {
            var result = await _unit.SubGenreRepository.GetAll();
            return result.ToList();
        }

        Task<IList<Genre.SubGenre>> ISubGenreService.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
