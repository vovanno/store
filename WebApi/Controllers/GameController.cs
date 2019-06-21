using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using CrossCuttingFunctionality.FilterModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApi.VIewDto;

namespace WebApi.Controllers
{
    [Route("api/games")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public GameController(IGameService gameService, IMapper mapper, ICommentService commentService)
        {
            _commentService = commentService;
            _gameService = gameService;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<GameViewDto>>> Get()
        {
            var gameList = await _gameService.GetAll();
            var result = _mapper.Map<IEnumerable<GameViewDto>>(gameList);
            return Ok(result);
        }

        [HttpGet]
        [Route("{id:int:min(1)}")]
        [AllowAnonymous]
        public async Task<ActionResult> GetGame(int id)
        {
            var result = await _gameService.GetById(id);
            return Ok(_mapper.Map<GameViewDto>(result));
        }

        [HttpPost]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult> AddGame([FromBody] GameViewDto game)
        {
            var createdGameId = await _gameService.Create(_mapper.Map<GameDto>(game));
            return Ok(createdGameId);
        }

        [HttpPut]
        [Authorize(Roles = "manager, publisher")]
        public async Task<ActionResult> EditGame(GameViewDto game)
        {
            var gameId = await _gameService.GetById(game.GameId);
            var currentUser = (ClaimsIdentity)User.Identity;
            var isPublisher = currentUser.FindFirst(p => p.Type == "PublisherId");
            if (currentUser.RoleClaimType != "manager" && isPublisher.Value != gameId.PublisherId.ToString())
            {
                return Forbid();
            }
            await _gameService.Edit(_mapper.Map<GameDto>(game));
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
        public async Task<ActionResult> GetGameGenres(int id)
        {
            var result = await _gameService.GetGameGenres(id);
            return Ok(_mapper.Map<IList<GenreViewDto>>(result));
        }

        [HttpPost]
        [Route("{id:int:min(1)}/comment")]
        [Authorize(Roles = "manager, user, publisher, moderator, admin")]
        public async Task<ActionResult> LeaveComment(int id, CommentViewDto comment)
        {
            comment.GameId = id;
            await _gameService.LeaveComment(id, _mapper.Map<CommentDto>(comment));
            return Ok();
        }

        [HttpGet]
        [Route("{id:int:min(1)}/comments")]
        [AllowAnonymous]
        public async Task<ActionResult> GetComments(int id)
        {
            var result = await _gameService.GetCommentsByGameId(id);
            return Ok(_mapper.Map<IList<CommentViewDto>>(result));
        }

        [HttpGet]
        [Route("filters")]
        [AllowAnonymous]
        public async Task<ActionResult> GetGamesWithFilters([FromQuery] FilterModel filter)
        {
            var result = await _gameService.GetGamesWithFilters(filter);
            return Ok(_mapper.Map<IList<GameViewDto>>(result));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("{id:int:min(1)}/comments/filter")]
        public async Task<ActionResult> GetCommentsWithFilters(int id, [FromQuery] FilterModel filter)
        {
            var result = await _commentService.GetCommentsWithFilters(id, filter);
            return Ok(_mapper.Map<IList<CommentViewDto>>(result));
        }

        [HttpPost]
        [Authorize(Roles = "manager, publisher")]
        [Route("{id:int:min(1)}/genres")]
        public async Task<ActionResult> AddGenresRange(int id, [FromBody] IList<GenreViewDto> genres)
        {
            var tempGenres = _mapper.Map<IList<GenreDto>>(genres);
            await _gameService.AddGenresRange(id, tempGenres);
            return StatusCode((int)HttpStatusCode.Created);
        }
    }
}
