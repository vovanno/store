using BLL.Interfaces;
using CrossCuttingFunctionality.FilterModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreApi.CommentApi;
using OnlineStoreApi.ProductApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICommentService _commentService;

        public ProductController(IProductService productService, ICommentService commentService)
        {
            _commentService = commentService;
            _productService = productService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<ProductResponse>>> Get()
        {
            var gamesList = await _productService.GetAll();
            return Ok(gamesList.Select(p => p.ToProductResponse()));
        }

        [HttpGet]
        [Route("{productId:int:min(1)}")]
        [AllowAnonymous]
        public async Task<ActionResult<ProductResponse>> GetGame(int productId)
        {
            var game = await _productService.GetById(productId);
            return Ok(game.ToProductResponse());
        }

        [HttpPost]
        //[Authorize(Roles = "manager")]
        public async Task<ActionResult<int>> AddGame([FromBody] CreateProductRequest request)
        {
            var gameModel = request.ToProductModel();

            var createdGameId = await _productService.Create(gameModel);
            return Ok(createdGameId);
        }

        [HttpPost("{productId:int:min(1)}/image")]
        //[Authorize(Roles = "manager")]
        public async Task<ActionResult<int>> UploadImages(int productId, [FromBody] UploadImageRequest request)
        {
            await _productService.UploadImages(productId, request.CreateImagesRequest);
            return Ok();
        }

        [HttpPut("{productId:int:min(1)}")]
        //[Authorize(Roles = "manager, publisher")]
        public async Task<ActionResult> EditGame(int productId, [FromBody] EditProductRequest request)
        {
            var gameModel = request.ToProductModel();

            await _productService.Edit(productId, gameModel);
            return Ok();
        }

        [HttpDelete]
        //[Authorize(Roles = "manager")]
        public async Task<ActionResult> Delete(int productId)
        {
            await _productService.Delete(productId);
            return Ok();
        }

        [HttpPost]
        [Route("{productId:int:min(1)}/comment")]
        //[Authorize(Roles = "manager, user, publisher, moderator, admin")]
        public async Task<IActionResult> LeaveComment(int productId, CreateCommentRequest request)
        {
            var commentModel = request.ToCommentModel();
            commentModel.ProductId = productId;
            commentModel.DateOfAdding = DateTime.UtcNow;

            await _commentService.Create(commentModel, request.ParentId);
            return Ok();
        }

        [HttpGet]
        [Route("{productId:int:min(1)}/comments")]
        [AllowAnonymous]
        public async Task<ActionResult<List<CommentResponse>>> GetComments(int productId)
        {
            var comments = await _commentService.GetCommentsByProductId(productId);
            return Ok(comments.Select(p => p.ToCommentResponse()));
        }

        [HttpGet]
        [Route("filters")]
        [AllowAnonymous]
        public async Task<ActionResult<List<ProductResponse>>> GetGamesWithFilters([FromQuery] FilterModel filter)
        {
            var games = await _productService.GetGamesWithFilters(filter);
            return Ok(games.Select(p => p.ToProductResponse()));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("{productId:int:min(1)}/comments/filter")]
        public async Task<ActionResult> GetCommentsWithFilters(int productId, [FromQuery] FilterModel filter)
        {
            var comments = await _commentService.GetCommentsWithFilters(productId, filter);
            return Ok(comments.Select(p => p.ToCommentResponse()));
        }

        //[HttpPost]
        //[Authorize(Roles = "manager, publisher")]
        //[Route("{productId:int:min(1)}/genres")]
        //public async Task<ActionResult> AddGenresRange(int productId, [FromBody] IList<GenreViewDto> genres)
        //{
        //    var tempGenres = _mapper.Map<IList<Genre>>(genres);
        //    await _productService.AddGenresRange(productId, tempGenres);
        //    return StatusCode((int)HttpStatusCode.Created);
        //}
    }
}
