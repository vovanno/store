using CrossCuttingFunctionality.FilterModels;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ICommentRepository
    {
        Task<IList<Comment>> GetCommentsWithFilters(int gameId, FilterModel filter);
        Task<IList<Comment>> GetCommentsByGameId(int gameId);
        Task<Comment> GetById(int id);
        Task<IList<Comment>> GetAll();
        Task<Comment> Add(Comment entity);
        Task Update(int id, Comment updatedEntity);
        Task Delete(int id);
    }
}
