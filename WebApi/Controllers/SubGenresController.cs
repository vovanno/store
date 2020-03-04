using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreApi.SubGenreApi;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubGenresController : ControllerBase
    {
        private readonly ISubGenreService _subGenreService;

        public SubGenresController(ISubGenreService subGenreService)
        {
            _subGenreService = subGenreService;
        }

        [HttpGet]
        public async Task<ActionResult<List<SubGenreResponse>>> Get()
        {
            var subGenres = await _subGenreService.GetAll();
            return Ok(subGenres.Select(p => p.ToSubGenreResponse()));
        }

        [HttpGet]
        [Route("{subGenreId:int:min(1)}")]
        public async Task<ActionResult<SubGenreResponse>> GetSubGenreById(int subGenreId)
        {
            var subGenre = await _subGenreService.GetById(subGenreId);
            return Ok(subGenre.ToSubGenreResponse());
        }

        [HttpPost]
        public async Task<ActionResult> AddGenre([FromBody] CreateSubGenreRequest request)
        {
            var subGenreModel = request.ToSubGenre();

            var subGenreId = await _subGenreService.Create(subGenreModel);
            return Ok(subGenreId);
        }

        [HttpPut("{subGenreId:int:min(1)}")]
        public async Task<ActionResult> EditGenre(int subGenreId, [FromBody] EditSubGenreRequest request)
        {
            var subGenreModel = request.ToSubGenre();

            await _subGenreService.Edit(subGenreId, subGenreModel);
            return Ok();
        }

        [HttpDelete]
        [Route("{subGenreId:int:min(1)}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _subGenreService.Delete(id);
            return Ok();
        }
    }
}