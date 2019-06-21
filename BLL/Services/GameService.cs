using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using CrossCuttingFunctionality.FilterModels;
using DAL.Entities;
using DAL.Exceptions;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        public GameService(IUnitOfWork unit, IMapper mapper)
        {
            _mapper = mapper;
            _unit = unit;
        }
        public async Task<int> Create(GameDto game)
        {
            if (game == null)
                throw new ArgumentNullException(nameof(game));
            var gameGenres = game.Genres;
            game.Genres = null;
            var temp = _mapper.Map<Game>(game);
            var resultGame = await _unit.GameRepository.Add(temp);
            await _unit.Commit();
            await _unit.GameRepository.AddGenresRange(resultGame.GameId, _mapper.Map<IList<Genre>>(gameGenres));
            await _unit.Commit();
            return resultGame.GameId;
        }

        public async Task Edit(GameDto game)
        {
            if (game.GameId < 0)
                throw new ArgumentException("Id can not be less than 0.");
            if (game == null)
                throw new ArgumentNullException(nameof(game));
            try
            {
                await _unit.GameRepository.Update(game.GameId, _mapper.Map<Game>(game));
            }
            catch (EntryNotFoundException e)
            {
                throw new ArgumentException("Entry does not found.", e);
            }

            var tempGenres = _mapper.Map<IList<Genre>>(game.Genres);
            await _unit.GameRepository.AddGenresRange(game.GameId, tempGenres);
            await _unit.Commit();
        }

        public async Task Delete(int id)
        {
            if (id < 0)
                throw new ArgumentException("Id can not be less than 0");
            await _unit.GameRepository.Delete(id);
            await _unit.Commit();
        }

        public async Task<GameDto> GetById(int id)
        {
            if (id < 0)
                throw new ArgumentException("Id can not be less than 0");
            var result = await _unit.GameRepository.GetById(id);
            if (result == null)
                throw new EntryNotFoundException($"Entry with id = {id} does not found");
            return _mapper.Map<GameDto>(result);
        }

        public async Task<IEnumerable<GameDto>> GetAll()
        {
            var result = await _unit.GameRepository.GetAll();
            return _mapper.Map<IEnumerable<GameDto>>(result);
        }

        public async Task<IList<GenreDto>> GetGameGenres(int gameId)
        {
            if (gameId < 0)
                throw new ArgumentException("Id can not be less than 0");
            var result = await _unit.GameRepository.GetGameGenres(gameId);
            return _mapper.Map<IList<GenreDto>>(result);
        }

        public async Task LeaveComment(int id, CommentDto comment)
        {
            if (id < 0)
                throw new ArgumentException("Id can not be less than 0");
            if (comment == null)
                throw new ArgumentNullException(nameof(comment));
            var result = _mapper.Map<Comment>(comment);
            await _unit.GameRepository.LeaveComment(id, result);
            await _unit.Commit();
        }

        public async Task<IList<CommentDto>> GetCommentsByGameId(int gameId)
        {
            if (gameId < 0)
                throw new ArgumentException("Id can not be less than 0");
            var result = await _unit.GameRepository.GetCommentsByGameId(gameId);
            return _mapper.Map<IList<CommentDto>>(result);
        }

        public async Task<IList<GameDto>> GetGamesWithFilters(FilterModel filter)
        {
            var result = await _unit.GameRepository.GetGamesWithFilters(filter);
            return _mapper.Map<IList<GameDto>>(result);
        }

        public async Task AddGenresRange(int gameId, IList<GenreDto> genres)
        {
            if (gameId < 0)
                throw new ArgumentException("Id can not be less than 0.");
            if (!genres.Any())
                return;
            if (genres.Any(p => p.GenreId < 1))
                throw new ArgumentException("You can not add genres that does not exist.");
            if (genres.Any(p => p.GenreId == 2) && genres.Count > 1)
                throw new ArgumentException("Game can have only default genre value or list of non-default genres.");
            var tempGenres = _mapper.Map<IList<Genre>>(genres);
            await _unit.GameRepository.AddGenresRange(gameId, tempGenres);
            await _unit.Commit();
        }
    }
}
