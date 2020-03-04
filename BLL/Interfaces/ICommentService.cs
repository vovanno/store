using CrossCuttingFunctionality.FilterModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace BLL.Interfaces
{
    public interface ICommentService
    {
        Task<IList<Comment>> GetCommentsWithFilters(int gameId, FilterModel filter);
        Task<int> Create(Comment entity);
        Task Edit(int id, Comment entity);
        Task Delete(int id);
        Task<Comment> GetById(int id);
        Task<IList<Comment>> GetAll();
    }
}
