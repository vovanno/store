using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreApi.GenresApi;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineStoreApi.GameApi;

namespace WebApi.Controllers
{
    [Route("api/genres")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService gameService)
        {
            _genreService = gameService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreResponse>>> Get()
        {
            var genresList = await _genreService.GetAll();

            return Ok(genresList.Select(p => p.ToGenreResponse()));
        }

        [HttpGet]
        [Route("{id:int:min(1)}")]
        public async Task<ActionResult<GenreResponse>> GetGenre(int id)
        {
            var genre = await _genreService.GetById(id);
            return Ok(genre.ToGenreResponse());
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddGenre(CreateGenreRequest request)
        {
            var genreModel = request.ToGenreModel();

            var createdGenreId = await _genreService.Create(genreModel);
            return Ok(createdGenreId);
        }

        [HttpPut("id:int:min(1)")]
        public async Task<ActionResult> EditGenre(int id, [FromBody] EditGenreRequest request)
        {
            var genreModel = request.ToGenreModel();

            await _genreService.Edit(id, genreModel);
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
        public async Task<ActionResult<List<GameResponse>>> GamesWithGenreId(int id)
        {
            var result = await _genreService.GamesWithGenreId(id);
            return Ok(result.Select(p => p.ToGameResponse()));
        }
    }
}