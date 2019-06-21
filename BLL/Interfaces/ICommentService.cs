using BLL.DTO;
using CrossCuttingFunctionality.FilterModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICommentService : IBaseService<CommentDto>
    {
        Task<IList<CommentDto>> GetCommentsWithFilters(int gameId, FilterModel filter);
    }
}
