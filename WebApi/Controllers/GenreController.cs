using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.VIewDto;

namespace WebApi.Controllers
{
    [Route("api/genres")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;

        public GenreController(IGenreService gameService, IMapper mapper)
        {
            _genreService = gameService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreViewDto>>> Get()
        {
            var gameList = await _genreService.GetAll();
            var result = _mapper.Map<IEnumerable<GenreViewDto>>(gameList);
            return Ok(result);
        }

        [HttpGet]
        [Route("{id:int:min(1)}")]
        public async Task<ActionResult> GetGenre(int id)
        {
            var result = await _genreService.GetById(id);
            return Ok(_mapper.Map<GenreViewDto>(result));
        }

        [HttpPost]
        public async Task<ActionResult> AddGenre(GenreViewDto game)
        {
            var createdGenreId = await _genreService.Create(_mapper.Map<GenreDto>(game));
            return Ok(createdGenreId);
        }

        [HttpPut]
        public async Task<ActionResult> EditGenre(GenreViewDto game)
        {
            await _genreService.Edit(_mapper.Map<GenreDto>(game));
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            await _genreService.Delete(id);
            return Ok();
        }

        [HttpGet]
        [Route("{id:int:min(1)}/games")]
        public async Task<ActionResult> GamesWithGenreId(int id)
        {
            var result = await _genreService.GamesWithGenreId(id);
            return Ok(_mapper.Map<IList<GameViewDto>>(result));
        }
    }
}