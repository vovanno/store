using System.Collections.Generic;
using System.Threading.Tasks;
using CrossCuttingFunctionality.FilterModels;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface ICommentRepository : IBaseRepository<Comment>
    {
        Task<IList<Comment>> GetCommentsWithFilters(int gameId, FilterModel filter);
    }
}
