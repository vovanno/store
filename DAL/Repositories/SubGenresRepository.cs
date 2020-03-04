using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Interfaces;
using Domain.Entities;

namespace DAL.Repositories
{
    internal class SubGenresRepository : ISubGenreRepository
    {
        private readonly AppContext _context;

        public SubGenresRepository(AppContext context)
        {
            _context = context;
        }

        public Task<Genre.SubGenre> GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<Genre.SubGenre>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<int> Add(Genre.SubGenre entity)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(int id, Genre.SubGenre updatedEntity)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
