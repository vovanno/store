using CrossCuttingFunctionality.FilterModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace BLL.Interfaces
{
    public interface ICommentService
    {
        Task<IList<Comment>> GetCommentsWithFilters(int productId, FilterModel filter);
        Task<IList<Comment>> GetCommentsByProductId(int productId);
        Task<int> Create(Comment entity, int? parentId);
        Task Edit(int commentId, Comment entity);
        Task Delete(int commentId);
        Task<Comment> GetById(int commentId);
        Task<IList<Comment>> GetAll();
    }
}
