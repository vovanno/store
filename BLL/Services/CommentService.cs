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

        public async Task<IList<Comment>> GetCommentsByProductId(int productId)
        {
            return await _unit.CommentRepository.GetCommentsByProductId(productId);
        }

        public async Task<int> Create(Comment comment, int? parentId)
        {
            Comment createdComment = null;

            if (parentId == null || parentId == 0)
                createdComment = await _unit.CommentRepository.Add(comment);
            else
            {
                var parent = await _unit.CommentRepository.GetById(parentId.Value);
                if(parent.Children == null)
                    parent.Children = new List<Comment>();
                parent.Children.Add(comment);
            }
            await _unit.Commit();
            return createdComment?.CommentId ?? 0;
        }

        public async Task Edit(int commentId, Comment comment)
        {
            await _unit.CommentRepository.Update(commentId, comment);
            await _unit.Commit();
        }

        public async Task Delete(int commentId)
        {
            await _unit.CommentRepository.Delete(commentId);
            await _unit.Commit();
        }

        public async Task<Comment> GetById(int commentId)
        {
            var result = await _unit.CommentRepository.GetById(commentId);
            return result;
        }

        public async Task<IList<Comment>> GetAll()
        {
            var result = await _unit.CommentRepository.GetAll();
            return result.ToList();
        }

        public async Task<IList<Comment>> GetCommentsWithFilters(int productId, FilterModel filter)
        {
            return await _unit.CommentRepository.GetCommentsWithFilters(productId, filter);
        }
    }
}
