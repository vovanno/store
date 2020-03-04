using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineStoreApi.GameApi;
using OnlineStoreApi.PublisherApi;

namespace WebApi.Controllers
{
    [Route("api/publisher")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService _publisherService;

        public PublisherController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [HttpGet]
        public async Task<ActionResult<List<PublisherResponse>>> Get()
        {
            var publishersList = await _publisherService.GetAll();
            return Ok(publishersList.Select(p => p.ToPublisherResponse()));
        }

        [HttpGet]
        [Route("{publisherId:int:min(1)}")]
        public async Task<ActionResult<PublisherResponse>> GetPublisher(int publisherId)
        {
            var result = await _publisherService.GetById(publisherId);
            return Ok(result.ToPublisherResponse());
        }

        [HttpPost]
        [AllowAnonymous]
        //[Authorize(Roles = "manager")]
        public async Task<ActionResult> AddPublisher([FromBody] CreatePublisherRequest request)
        {
            var publisherModel = request.ToPublisherModel();

            var createdPublisherId = await _publisherService.Create(publisherModel);
            return Ok(createdPublisherId);
        }

        [HttpPut("{publisherId:int:min(1)}")]
        [Authorize(Roles = "publisher")]
        public async Task<ActionResult> EditPublisher(int publisherId, [FromBody] EditPublisherRequest request)
        {
            var publisherModel = request.ToPublisherModel();

            await _publisherService.Edit(publisherId, publisherModel);
            return Ok();
        }

        [HttpDelete]
        [Route("{publisherId:int:min(1)}")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult> Delete(int publisherId)
        {
            await _publisherService.Delete(publisherId);
            return Ok();
        }

        [HttpGet]
        [Route("{publisherId:int:min(1)}/games")]
        [ProducesResponseType(typeof(List<GameResponse>), 200)]
        public async Task<ActionResult<List<GameResponse>>> GetGamesByPublisherId(int publisherId)
        {
            var games = await _publisherService.GetGamesByPublisherId(publisherId);
            var response = games.Select(p => p.ToGameResponse());
            return Ok(response);
        }
    }
}