using BLL.Interfaces;
using CrossCuttingFunctionality.FilterModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreApi.CommentApi;
using OnlineStoreApi.GenresApi;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineStoreApi.GameApi;

namespace WebApi.Controllers
{
    [Route("api/games")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly ICommentService _commentService;

        public GameController(IGameService gameService, ICommentService commentService)
        {
            _commentService = commentService;
            _gameService = gameService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<GameResponse>>> Get()
        {

            var test = new List<GameResponse>();
            var rr = test.SelectMany(p => p.Genres);

            var pp = rr.Select(p => p.Name);

            var gamesList = await _gameService.GetAll();
            return Ok(gamesList.Select(p => p.ToGameResponse()));
        }

        [HttpGet]
        [Route("{id:int:min(1)}")]
        [AllowAnonymous]
        public async Task<ActionResult<GameResponse>> GetGame(int id)
        {
            var game = await _gameService.GetById(id);
            return Ok(game.ToGameResponse());
        }

        [HttpPost]
        //[Authorize(Roles = "manager")]
        public async Task<ActionResult<int>> AddGame([FromBody] CreateGameRequest request)
        {
            var gameModel = request.ToGameModel();

            var createdGameId = await _gameService.Create(gameModel, request.GenresIds, request.PlatformsIds);
            return Ok(createdGameId);
        }

        [HttpPut("id:int:min(1)")]
        [Authorize(Roles = "manager, publisher")]
        public async Task<ActionResult> EditGame(int id, [FromBody] EditGameRequest request)
        {
            var gameModel = request.ToGameModel();

            await _gameService.Edit(id, gameModel);
            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult> Delete(int id)
        {
            await _gameService.Delete(id);
            return Ok();
        }

        [HttpGet]
        [Route("{id:int:min(1)}/genres")]
        [AllowAnonymous]
        public async Task<ActionResult<List<GenreResponse>>> GetGameGenres(int id)
        {
            var gameGenres = await _gameService.GetGameGenres(id);
            return Ok(gameGenres.Select(p => p.ToGenreResponse()));
        }

        [HttpPost]
        [Route("{id:int:min(1)}/comment")]
        [Authorize(Roles = "manager, user, publisher, moderator, admin")]
        public async Task<ActionResult> LeaveComment(int id, CreateCommentRequest request)
        {
            var commentModel = request.ToCommentModel();

            await _gameService.LeaveComment(id, commentModel);
            return Ok();
        }

        [HttpGet]
        [Route("{gameId:int:min(1)}/comments")]
        [AllowAnonymous]
        public async Task<ActionResult<List<CommentResponse>>> GetComments(int gameId)
        {
            var comments = await _gameService.GetCommentsByGameId(gameId);
            return Ok(comments.Select(p => p.ToCommentResponse()));
        }

        [HttpGet]
        [Route("filters")]
        [AllowAnonymous]
        public async Task<ActionResult<List<GameResponse>>> GetGamesWithFilters([FromQuery] FilterModel filter)
        {
            var games = await _gameService.GetGamesWithFilters(filter);
            return Ok(games.Select(p => p.ToGameResponse()));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("{gameId:int:min(1)}/comments/filter")]
        public async Task<ActionResult> GetCommentsWithFilters(int gameId, [FromQuery] FilterModel filter)
        {
            var comments = await _commentService.GetCommentsWithFilters(gameId, filter);
            return Ok(comments.Select(p => p.ToCommentResponse()));
        }

        //[HttpPost]
        //[Authorize(Roles = "manager, publisher")]
        //[Route("{id:int:min(1)}/genres")]
        //public async Task<ActionResult> AddGenresRange(int id, [FromBody] IList<GenreViewDto> genres)
        //{
        //    var tempGenres = _mapper.Map<IList<Genre>>(genres);
        //    await _gameService.AddGenresRange(id, tempGenres);
        //    return StatusCode((int)HttpStatusCode.Created);
        //}
    }
}
