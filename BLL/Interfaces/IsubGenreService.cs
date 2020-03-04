using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace BLL.Interfaces
{
    public interface ISubGenreService
    {
        Task<int> Create(Genre.SubGenre entity);
        Task Edit(int id, Genre.SubGenre entity);
        Task Delete(int id);
        Task<Genre.SubGenre> GetById(int id);
        Task<IList<Genre.SubGenre>> GetAll();
    }
}
