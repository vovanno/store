using BLL.DTO;
using CrossCuttingFunctionality.FilterModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IGameService : IBaseService<GameDto>
    {
        Task<IList<GenreDto>> GetGameGenres(int gameId);
        Task LeaveComment(int id, CommentDto comment);
        Task<IList<CommentDto>> GetCommentsByGameId(int gameId);
        Task<IList<GameDto>> GetGamesWithFilters(FilterModel filter);
        Task AddGenresRange(int gameId, IList<GenreDto> genres);
    }
}
