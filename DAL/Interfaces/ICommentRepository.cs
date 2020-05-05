using CrossCuttingFunctionality.FilterModels;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ICommentRepository
    {
        Task<IList<Comment>> GetCommentsWithFilters(int productId, FilterModel filter);
        Task<IList<Comment>> GetCommentsByProductId(int productId);
        Task<Comment> GetById(int commentId);
        Task<IList<Comment>> GetAll();
        Task<Comment> Add(Comment entity);
        Task Update(int commentId, Comment updatedEntity);
        Task Delete(int commentId);
    }
}
