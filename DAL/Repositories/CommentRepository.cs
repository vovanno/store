using CrossCuttingFunctionality.FilterModels;
using DAL.Entities;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    internal sealed class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(IAppContext context) : base(context)
        {

        }

        public async Task<IList<Comment>> GetCommentsWithFilters(int gameId, FilterModel filter)
        {
            IQueryable<Comment> result = null;
            if (filter.ByDateAscending)
            {
                result = await Task.Run(() =>
                    Db.Where(p => p.GameId == gameId).OrderBy(p => p.DateOfAdding));
            }
            else if (filter.ByDateDescending)
            {
                result = await Task.Run(() =>
                    Db.Where(p => p.GameId == gameId).OrderByDescending(p => p.AmountOfLikes));
            }
            else if (filter.IsMostPopular)
            {
                result = await Task.Run(() =>
                    Db.Where(p => p.GameId == gameId).OrderBy(p => p.AmountOfLikes));
            }
            return result?.Skip((filter.Page - 1) * filter.Size).Take(filter.Size).ToList();
        }
    }
}
