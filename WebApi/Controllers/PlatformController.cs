using System;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreApi.PlatformApi;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/platform")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private readonly IPlatformService _platformService;

        public PlatformController(IPlatformService platformService)
        {
            _platformService = platformService;
        }

        [HttpGet]
        public async Task<ActionResult<List<PlatformResponse>>> Get()
        {
            var platformsList = await _platformService.GetAll();
            return Ok(platformsList.Select(p => p.ToPlatformResponse()));
        }

        [HttpGet]
        [Route("{platformId:int:min(1)}")]
        public async Task<ActionResult<PlatformResponse>> GetPlatform(int platformId)
        {
            var platform = await _platformService.GetById(platformId);
            return Ok(platform.ToPlatformResponse());
        }

        [HttpPost]
        public async Task<ActionResult> AddPlatform([FromBody] CreatePlatformRequest request)
        {
            var platformModel = request.ToPlatformTypeModel();

            var createdPlatformId = await _platformService.Create(platformModel);
            return Ok(createdPlatformId);
        }

        [HttpPut("{platformId:int:min(1)}")]
        public async Task<ActionResult> EditPlatform(int platformId, [FromBody] EditPlatformRequest request)
        {
            var platformModel = request.ToPlatformTypeModel();

            await _platformService.Edit(platformId, platformModel);
            return Ok();
        }

        [HttpDelete]
        [Route("{platformId:int:min(1)}")]
        public async Task<ActionResult> Delete(int platformId)
        {
            await _platformService.Delete(platformId);
            return Ok();
        }
    }
}