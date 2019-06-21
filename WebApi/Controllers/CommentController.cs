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
    [Route("api/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentController(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentViewDto>>> Get()
        {
            var commentList = await _commentService.GetAll();
            var result = _mapper.Map<IEnumerable<CommentViewDto>>(commentList);
            return Ok(result);
        }

        [HttpGet]
        [Route("{id:int:min(1)}")]
        public async Task<ActionResult> GetComment(int id)
        {
            var result = await _commentService.GetById(id);
            return Ok(_mapper.Map<CommentViewDto>(result));
        }

        [HttpPost]
        [Route("{id:int:min(1)}/comment")]
        public async Task<ActionResult> AddComment(int id, [FromBody] CommentViewDto comment)
        {
            comment.ParentId = id;
            var createdCommentId = await _commentService.Create(_mapper.Map<CommentDto>(comment));
            return Ok(createdCommentId);
        }

        [HttpPut]
        [Authorize(Roles = "moderator")]
        public async Task<ActionResult> EditComment([FromBody] CommentViewDto comment)
        {
            await _commentService.Edit(_mapper.Map<CommentDto>(comment));
            return Ok();
        }

        [HttpDelete]
        [Route("{id:int:min(1)}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _commentService.Delete(id);
            return Ok();
        }
    }
}