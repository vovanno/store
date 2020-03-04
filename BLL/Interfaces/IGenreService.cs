using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Dtos;
using Domain.Entities;

namespace BLL.Interfaces
{
    public interface IGenreService
    {
        Task<IList<GameDto>> GamesWithGenreId(int id);
        Task<int> Create(Genre entity);
        Task Edit(int id, Genre entity);
        Task Delete(int id);
        Task<Genre> GetById(int id);
        Task<IList<Genre>> GetAll();

    }
}
