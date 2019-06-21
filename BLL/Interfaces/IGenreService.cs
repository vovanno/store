using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IGenreService : IBaseService<GenreDto>
    {
        Task<IList<GameDto>> GamesWithGenreId(int id);
    }
}
