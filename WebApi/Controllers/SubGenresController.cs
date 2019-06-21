using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApi.VIewDto;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubGenresController : ControllerBase
    {
        private readonly ISubGenreService _subGenreService;
        private readonly IMapper _mapper;

        public SubGenresController(ISubGenreService subGenreService, IMapper mapper)
        {
            _subGenreService = subGenreService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubGenreViewDto>>> Get()
        {
            var commentList = await _subGenreService.GetAll();
            var result = _mapper.Map<IEnumerable<SubGenreViewDto>>(commentList);
            return Ok(result);
        }

        [HttpGet]
        [Route("{id:int:min(1)}")]
        public async Task<ActionResult> GetGenreById(int id)
        {
            var result = await _subGenreService.GetById(id);
            return Ok(_mapper.Map<SubGenreViewDto>(result));
        }

        [HttpPost]
        public async Task<ActionResult> AddGenre( [FromBody] SubGenreViewDto subGenre)
        {
            var createdCommentId = await _subGenreService.Create(_mapper.Map<SubGenreDto>(subGenre));
            return Ok(createdCommentId);
        }

        [HttpPut]
        public async Task<ActionResult> EditGenre([FromBody] SubGenreViewDto subGenre)
        {
            await _subGenreService.Edit(_mapper.Map<SubGenreDto>(subGenre));
            return Ok();
        }

        [HttpDelete]
        [Route("{id:int:min(1)}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _subGenreService.Delete(id);
            return Ok();
        }
    }
}