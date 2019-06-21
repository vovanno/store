using CrossCuttingFunctionality.FilterModels;
using DAL.Entities;
using DAL.Exceptions;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    internal sealed class GameRepository : GenericRepository<Game>, IGameRepository
    {
        public GameRepository(IAppContext context) : base(context)
        {

        }

        public override async Task<IEnumerable<Game>> GetAll()
        {
            return await Db.Where(p => !p.IsDeleted).ToListAsync();
        }

        public async Task AddGenresRange(int gameId, IList<Genre> genres)
        {
            var game = await Db.FirstOrDefaultAsync(p => !p.IsDeleted && p.GameId == gameId);
            if (game == null)
                return;
            if (game.GameGenres == null)
                game.GameGenres = new List<GameGenre>();
            else
            {
                game.GameGenres.Clear();
            }
            foreach (var genre in genres)
            {
                game.GameGenres.Add(new GameGenre()
                {
                    GameId = gameId,
                    GenreId = genre.GenreId
                });
            }
            Db.Update(game);
        }

        public override async Task Update(int id, Game updatedGame)
        {
            var modifiedObj = await Db.FirstOrDefaultAsync(p => !p.IsDeleted && p.GameId == id);
            if (modifiedObj == null)
                throw new EntryNotFoundException("Entry with the given key doesn't exist or was deleted earlier.");
            Context.Entry(modifiedObj).CurrentValues.SetValues(updatedGame);
        }

        public async Task<IList<GameGenre>> GetGameGenres(int gameId)
        {
            var result = await Db.FindAsync(gameId);
            return result.GameGenres;
        }

        public async Task LeaveComment(int gameId, Comment comment)
        {
            var game = await Db.FirstOrDefaultAsync(p => !p.IsDeleted && p.GameId == gameId);
            if (game == null)
                return;
            game.Comments.Add(comment);
            Db.Update(game);
        }

        public async Task<IList<Comment>> GetCommentsByGameId(int gameId)
        {
            var result = await Db.FindAsync(gameId);
            return result.Comments;
        }

        public async Task<IList<Game>> GetGamesWithFilters(FilterModel filter)
        {
            var result = await Task.Run(() =>
                Db.Where(p => p.GameGenres.Any(n => filter.Genres.Contains(n.Genre.Name)) &&
                            p.PlatformTypes.Any(n => filter.Platforms.Contains(n.PlatformType.Type)) &&
                            filter.Publishers.Contains(p.Publisher.Name) &&
                            p.DateOfAdding >= filter.DateOfAdding &&
                            p.Price > filter.PriceFilter.From && p.Price <= filter.PriceFilter.To));
            if (filter.IsMostCommented)
            {
                result = result.OrderBy(p => p.Comments.Count);
            }
            else if (filter.IsMostPopular)
            {
                result = result.OrderBy(p => p.AmountOfViews);
            }
            else if (filter.ByPriceAscending)
            {
                result = result.OrderBy(p => p.Price);
            }
            else if (filter.ByPriceDescending)
            {
                result = result.OrderByDescending(p => p.Price);
            }
            else if (filter.ByDateAscending)
            {
                result = result.OrderBy(p => p.DateOfAdding);
            }
            else if (filter.ByDateDescending)
            {
                result = result.OrderByDescending(p => p.DateOfAdding);
            }
            return result.Skip((filter.Page - 1) * filter.Size).Take(filter.Size).ToList();
        }
    }
}
