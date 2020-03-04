using BLL.Interfaces;
using CrossCuttingFunctionality.FilterModels;
using DAL.Interfaces;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unit;
        public CommentService(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task<int> Create(Comment comment)
        {
            var createdComment = await _unit.CommentRepository.Add(comment);
            await _unit.Commit();
            return createdComment.CommentId;
        }

        public async Task Edit(int id, Comment comment)
        {
            await _unit.CommentRepository.Update(comment.CommentId, comment);
            await _unit.Commit();
        }

        public async Task Delete(int id)
        {
            await _unit.CommentRepository.Delete(id);
            await _unit.Commit();
        }

        public async Task<Comment> GetById(int id)
        {
            var result = await _unit.CommentRepository.GetById(id);
            return result;
        }

        public async Task<IList<Comment>> GetAll()
        {
            var result = await _unit.CommentRepository.GetAll();
            return result.ToList();
        }

        public async Task<IList<Comment>> GetCommentsWithFilters(int gameId, FilterModel filter)
        {
            var result = await _unit.CommentRepository.GetCommentsWithFilters(gameId, filter);
            return result;
        }
    }
}
