using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace DAL.Interfaces
{
    public interface ISubGenreRepository
    {
        Task<Genre.SubGenre> GetById(int id);
        Task<IList<Genre.SubGenre>> GetAll();
        Task<int> Add(Genre.SubGenre entity);
        Task Update(int id, Genre.SubGenre updatedEntity);
        Task Delete(int id);
    }
}
