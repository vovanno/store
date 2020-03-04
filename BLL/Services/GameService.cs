using BLL.Interfaces;
using CrossCuttingFunctionality.FilterModels;
using DAL.Interfaces;
using Domain.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork _unit;
        public GameService(IUnitOfWork unit)
        {
            _unit = unit;
        }
        public async Task<int> Create(Game game, int[] genresIds, int[] platformsIds)
        {
            var createdGame = await _unit.GameRepository.Add(game, genresIds, platformsIds);
            await _unit.Commit();
            return createdGame.GameId;
        }

        public async Task Edit(int gameId, Game game)
        {
            await _unit.GameRepository.Update(game.GameId, game);
            await _unit.Commit();
        }

        public async Task Delete(int gameId)
        {
            await _unit.GameRepository.Delete(gameId);
            await _unit.Commit();
        }

        public async Task<Game> GetById(int gameId)
        {
            var result = await _unit.GameRepository.GetById(gameId);
            return result;
        }

        public async Task<List<GameDto>> GetAll()
        {
            var games = await _unit.GameRepository.GetAll();
            var genres = await _unit.GenreRepository.GetAll();
            var platforms = await _unit.PlatformRepository.GetAll();

            var mappedGames = games.Select(p =>
            {
                var genresIds = p.GameGenres.Select(x => x.GenreId).ToList();
                var platformsIds = p.PlatformTypes.Select(x => x.PlatformTypeId).ToList();

                return new GameDto
                {
                    GameId = p.GameId,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    DateOfAdding = p.DateOfAdding,
                    AmountOfViews = p.AmountOfViews,
                    Publisher = p.Publisher,
                    Comments = p.Comments,
                    PlatformTypes = platforms.Where(x => platformsIds.Contains(x.PlatformTypeId)).ToList(),
                    GameGenres = genres.Where(x => genresIds.Contains(x.GenreId)).ToList()
                };
            }).ToList();
            return mappedGames;
        }

        public async Task<IList<Genre>> GetGameGenres(int gameId)
        {
            var result = await _unit.GameRepository.GetGameGenres(gameId);
            return result;
        }

        public async Task LeaveComment(int gameId, Comment comment)
        {
            await _unit.GameRepository.LeaveComment(gameId, comment);
            await _unit.Commit();
        }

        public async Task<IList<Comment>> GetCommentsByGameId(int gameId)
        {
            var result = await _unit.CommentRepository.GetCommentsByGameId(gameId);
            return result;
        }

        public async Task<IList<Game>> GetGamesWithFilters(FilterModel filter)
        {
            var result = await _unit.GameRepository.GetGamesWithFilters(filter);
            return result;
        }

        public async Task AddGenresRange(int gameId, IList<Genre> genres)
        {
            if (!genres.Any())
                return;
            if (genres.Any(p => p.GenreId < 1))
                throw new ArgumentException("You can not add genres that does not exist.");
            if (genres.Any(p => p.GenreId == 2) && genres.Count > 1)
                throw new ArgumentException("Game can have only default genre value or list of non-default genres.");
            await _unit.GameRepository.AddGenresRange(gameId, genres.Select(p => p.GenreId).ToArray());
            await _unit.Commit();
        }
    }
}
