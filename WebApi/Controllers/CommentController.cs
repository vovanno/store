using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreApi.CommentApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentResponse>>> Get()
        {
            var comments = await _commentService.GetAll();
            var commentsResponse = comments.Select(p => p.ToCommentResponse());
            return Ok(commentsResponse);
        }

        [HttpGet]
        [Route("{id:int:min(1)}")]
        public async Task<ActionResult<CommentResponse>> GetComment(int id)
        {
            var comment = await _commentService.GetById(id);
            return Ok(comment.ToCommentResponse());
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddComment(CreateCommentRequest request)
        {
            var commentModel = request.ToCommentModel();
            commentModel.DateOfAdding = DateTime.UtcNow;

            var createdCommentId = await _commentService.Create(commentModel);
            return Ok(createdCommentId);
        }

        [HttpPut("{id:int:min(1)}")]
        [Authorize(Roles = "moderator")]
        public async Task<ActionResult> EditComment(int id, [FromBody] EditCommentRequest request)
        {
            var commentModel = request.ToCommentModel();

            await _commentService.Edit(id, commentModel);
            return NoContent();
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