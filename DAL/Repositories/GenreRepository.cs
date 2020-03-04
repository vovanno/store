using DAL.Interfaces;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Exceptions;
using Domain;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace DAL.Repositories
{
    internal sealed class GenreRepository : IGenreRepository
    {
        private readonly AppContext _context;
        public GenreRepository(AppContext context)
        {
            _context = context;
        }

        public async Task<IList<Game>> GamesWithGenreId(int genreId)
        {
            var games = await _context.Games.AsNoTracking()
                .Include(p => p.GameGenres.Where(x => x.GenreId == genreId))
                .Include(p => p.PlatformTypes)
                .Include(p => p.Comments)
                .ToListAsync();
            return games;
        }

        public async Task<Genre> GetById(int genreId)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(p => p.GenreId == genreId);

            return genre ?? throw new EntryNotFoundException($"Genre with GenreId={genreId} was not found");
        }

        public async Task<IList<Genre>> GetAll()
        {
            return await _context.Genres.AsNoTracking().ToListAsync();
        }

        public async Task<Genre> Add(Genre entity)
        {
            var genre = await _context.Genres.AddAsync(entity);
            return genre.Entity;
        }

        public async Task Update(int genreId, Genre updatedEntity)
        {
            var foundGenre = await _context.Genres.FirstOrDefaultAsync(p => p.GenreId == genreId);

            if(foundGenre == null)
                throw new EntryNotFoundException($"Genre with GenreId={genreId} was not found");

            _context.Entry(foundGenre).CurrentValues.SetValues(updatedEntity.ToSqlUpdateParameters());
        }

        public async Task Delete(int genreId)
        {
            await _context.Database.ExecuteSqlCommandAsync("DELETE FROM Genres WHERE GenreId=@genreId",
                new MySqlParameter("@genreId", genreId));
        }

        public async Task AddGenresRange(int gameId, int[] genreIds)
        {
            var gameGenres = genreIds.Select(p => new GameGenre {GameId = gameId, GenreId = p}).ToArray();
            await _context.GameGenres.AddRangeAsync(gameGenres);
        }
    }
}
