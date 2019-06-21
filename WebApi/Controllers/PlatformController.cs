using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.VIewDto;

namespace WebApi.Controllers
{
    [Route("api/platform")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private readonly IPlatformService _platformService;
        private readonly IMapper _mapper;

        public PlatformController(IPlatformService platformService, IMapper mapper)
        {
            _platformService = platformService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlatformViewDto>>> Get()
        {
            var platformList = await _platformService.GetAll();
            var result = _mapper.Map<IEnumerable<PlatformViewDto>>(platformList);
            return Ok(result);
        }

        [HttpGet]
        [Route("{id:int:min(1)}")]
        public async Task<ActionResult> GetPlatform(int id)
        {
            var result = await _platformService.GetById(id);
            return Ok(_mapper.Map<PlatformViewDto>(result));
        }

        [HttpPost]
        public async Task<ActionResult> AddPlatform([FromBody] PlatformViewDto platform)
        {
            var createdPlatformId = await _platformService.Create(_mapper.Map<PlatformTypeDto>(platform));
            return Ok(createdPlatformId);
        }

        [HttpPut]
        public async Task<ActionResult> EditPlatform([FromBody] PlatformViewDto platform)
        {
            await _platformService.Edit(_mapper.Map<PlatformTypeDto>(platform));
            return Ok();
        }

        [HttpDelete]
        [Route("{id:int:min(1)}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _platformService.Delete(id);
            return Ok();
        }
    }
}