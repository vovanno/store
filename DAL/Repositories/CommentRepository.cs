using DAL.Interfaces;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrossCuttingFunctionality.FilterModels;
using DAL.Context;
using DAL.Exceptions;
using Domain;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace DAL.Repositories
{
    internal sealed class CommentRepository : ICommentRepository
    {
        private readonly StoreContext _context;
        public CommentRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IList<Comment>> GetCommentsWithFilters(int productId, FilterModel filter)
        {
            List<Comment> result = null;
            if (filter.ByDateAscending)
            {
                result = await _context.Comments.AsNoTracking().Where(p => p.ProductId == productId).OrderBy(p => p.DateOfAdding).ToListAsync();
            }
            else if (filter.ByDateDescending)
            {
                result = await _context.Comments.AsNoTracking().Where(p => p.ProductId == productId).OrderByDescending(p => p.AmountOfLikes).ToListAsync();
            }
            else if (filter.IsMostPopular)
            {
                result = await _context.Comments.Where(p => p.ProductId == productId).OrderBy(p => p.AmountOfLikes).ToListAsync();
            }

            return result?.Skip((filter.Page - 1) * filter.Size).Take(filter.Size).ToList();
        }

        public async Task<IList<Comment>> GetCommentsByProductId(int productId)
        {
            return await _context.Comments.FromSql(
                "SELECT * FROM Comments WHERE ProductId=@productId", new MySqlParameter("@productId", productId)).ToListAsync();
        }

        public async Task<Comment> GetById(int commentId)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(p => p.CommentId == commentId);
            
            if(comment == null)
                throw new EntryNotFoundException($"Comment with CommentId={commentId} was not found");

            return comment;
        }

        public async Task<IList<Comment>> GetAll()
        {
            return await _context.Comments.AsNoTracking().ToListAsync();
        }

        public async Task<Comment> Add(Comment comment)
        {
            var createdComment = await _context.Comments.AddAsync(comment);
            return createdComment.Entity;
        }

        public async Task Update(int commentId, Comment updatedEntity)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(p => p.CommentId == commentId);

            if (comment == null)
                throw new EntryNotFoundException($"Comment with CommentId={commentId} was not found");

            _context.Entry(comment).CurrentValues.SetValues(updatedEntity.ToSqlUpdateParameters());
        }

        public async Task Delete(int commentId)
        {
            await _context.Database.ExecuteSqlCommandAsync("DELETE FROM Categories WHERE CategoryId=@commentId",
                new MySqlParameter("@commentId", commentId));
        }
    }
}
