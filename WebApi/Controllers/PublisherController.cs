using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using WebApi.VIewDto;

namespace WebApi.Controllers
{
    [Route("api/publisher")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService _publisherService;
        private readonly IMapper _mapper;

        public PublisherController(IPublisherService publisherService, IMapper mapper)
        {
            _publisherService = publisherService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublisherViewDto>>> Get()
        {
            var publisherList = await _publisherService.GetAll();
            var result = _mapper.Map<IEnumerable<PublisherViewDto>>(publisherList);
            return Ok(result);
        }

        [HttpGet]
        [Route("{id:int:min(1)}")]
        public async Task<ActionResult> GetPublisher(int id)
        {
            var result = await _publisherService.GetById(id);
            return Ok(_mapper.Map<PublisherViewDto>(result));
        }

        [HttpPost]
        [AllowAnonymous]
        //[Authorize(Roles = "manager")]
        public async Task<ActionResult> AddPublisher([FromBody] PublisherViewDto publisher)
        {
            var createdPublisherId = await _publisherService.Create(_mapper.Map<PublisherDto>(publisher));
            return Ok(createdPublisherId);
        }

        [HttpPut]
        [Authorize(Roles = "publisher")]
        public async Task<ActionResult> EditPublisher([FromBody] PublisherViewDto publisher)
        {
            await _publisherService.Edit(_mapper.Map<PublisherDto>(publisher));
            return Ok();
        }

        [HttpDelete]
        [Route("{id:int:min(1)}")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult> Delete(int id)
        {
            await _publisherService.Delete(id);
            return Ok();
        }

        [HttpGet]
        [Route("{id:int:min(1)}/games")]
        public async Task<ActionResult> GetGamesByPublisherId(int id)
        {
            var result = await _publisherService.GetGamesByPublisherId(id);
            return Ok(result);
        }
    }
}