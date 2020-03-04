using CrossCuttingFunctionality.FilterModels;
using DAL.Exceptions;
using DAL.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppContext = DAL.Context.AppContext;

namespace DAL.Repositories
{
    internal sealed class GameRepository : IGameRepository
    {
        private readonly AppContext _context;
        public GameRepository(AppContext context)
        {
            _context = context;
        }

        public async Task<Game> GetById(int gameId)
        {
            var game = await _context.Games.FirstOrDefaultAsync(p => p.GameId == gameId);
            return game ?? throw new EntryNotFoundException($"Game with GameId={gameId} was not found");
        }

        public async Task<IList<Game>> GetAll()
        {
            var result =  await _context.Games.AsNoTracking()
                .Include(p => p.Publisher)
                .Include(p => p.GameGenres)
                .Include(p => p.Comments)
                .ToListAsync();
            return result;
        }

        public async Task<Game> Add(Game game, int[] genresIds, int[] platformsIds)
        {
            var gameEntity = await _context.Games.AddAsync(game);
            var gameId = gameEntity.Entity.GameId;

            var gameGenres = genresIds.Select(p => new GameGenre {GenreId = p, GameId = gameId }).ToList();
            var gamePlatforms = platformsIds.Select(p => new GamePlatform {GameId = gameId, PlatformTypeId = p}).ToList();

            gameEntity.Entity.GameGenres = gameGenres;
            gameEntity.Entity.PlatformTypes = gamePlatforms;

            return gameEntity.Entity;
        }

        public async Task AddGenresRange(int gameId, int[] genreIds)
        {
            var parameters = genreIds.Select(p => new GameGenre {GameId = gameId, GenreId = p});
            await _context.GameGenres.AddRangeAsync(parameters);
        }

        public async Task Update(int gameId, Game updatedGame)
        {
            var modifiedObj = await _context.Games.FirstOrDefaultAsync(p => p.GameId == gameId);

            _context.Entry(modifiedObj).CurrentValues.SetValues(updatedGame);
        }

        public async Task Delete(int gameId)
        {
            await _context.Database.ExecuteSqlCommandAsync("DELETE FROM Games WHERE GameId=@gameId",
                new MySqlParameter("@gameId", gameId));
        }

        public async Task<IList<Genre>> GetGameGenres(int gameId)
        {
            var result = await _context.Genres.FromSql(
                "SELECT ge.GenreId, ge.Name FROM Genres ge INNER JOIN GameGenres gg ON ge.GenreId = gg.GenreId WHERE gg.GameId = @gameId",
                new MySqlParameter("@gameId", gameId)).ToListAsync();
            return result;
        }

        public async Task LeaveComment(int gameId, Comment comment)
        {
            await _context.Comments.AddAsync(comment);
        }

        public async Task<IList<Game>> GetGamesWithFilters(FilterModel filter)
        {
            IOrderedEnumerable<Game> filteredResult = null; 

            var result = await _context.Games.Where(p => p.GameGenres.Any(n => filter.Genres.Contains(n.Genre.Name)) &&
                            p.PlatformTypes.Any(n => filter.Platforms.Contains(n.PlatformType.Type)) &&
                            filter.Publishers.Contains(p.Publisher.Name) &&
                            p.DateOfAdding >= filter.DateOfAdding &&
                            p.Price > filter.PriceFilter.From && p.Price <= filter.PriceFilter.To).ToListAsync();

            if (filter.IsMostCommented)
            {
                filteredResult = result.OrderBy(p => p.Comments.Count);
            }
            else if (filter.IsMostPopular)
            {
                filteredResult = result.OrderBy(p => p.AmountOfViews);
            }
            else if (filter.ByPriceAscending)
            {
                filteredResult = result.OrderBy(p => p.Price);
            }
            else if (filter.ByPriceDescending)
            {
                filteredResult = result.OrderByDescending(p => p.Price);
            }
            else if (filter.ByDateAscending)
            {
                filteredResult = result.OrderBy(p => p.DateOfAdding);
            }
            else if (filter.ByDateDescending)
            {
                filteredResult = result.OrderByDescending(p => p.DateOfAdding);
            }
            return filteredResult?.Skip((filter.Page - 1) * filter.Size).Take(filter.Size).ToList();
        }
    }
}
