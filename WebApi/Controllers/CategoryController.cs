using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineStoreApi.CategoryApi;
using OnlineStoreApi.ProductApi;

namespace WebApi.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public CategoryController(ICategoryService gameService, IProductService productService)
        {
            _categoryService = gameService;
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryResponse>>> Get()
        {
            var genresList = await _categoryService.GetAll();

            return Ok(genresList.Select(p => p.ToCategoryResponse()));
        }

        [HttpGet]
        [Route("{categoryId:int:min(1)}")]
        public async Task<ActionResult<CategoryResponse>> GetGenre(int categoryId)
        {
            var genre = await _categoryService.GetById(categoryId);
            return Ok(genre.ToCategoryResponse());
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddGenre(CreateCategoryRequest request)
        {
            var genreModel = request.ToCategoryModel();

            var createdGenreId = await _categoryService.Create(genreModel);
            return Ok(createdGenreId);
        }

        [HttpPut("{categoryId:int:min(1)}")]
        public async Task<ActionResult> EditGenre(int categoryId, [FromBody] EditCategoryRequest request)
        {
            var genreModel = request.ToCategoryModel();

            await _categoryService.Edit(categoryId, genreModel);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int categoryId)
        {
            await _categoryService.Delete(categoryId);
            return Ok();
        }

        [HttpGet]
        [Route("{categoryId:int:min(1)}/products")]
        public async Task<ActionResult<List<ProductResponse>>> GetProductsByCategory(int categoryId)
        {
            var result = await _productService.GetProductsByCategoryId(categoryId);
            return Ok(result.Select(p => p.ToProductResponse()));
        }
    }
}