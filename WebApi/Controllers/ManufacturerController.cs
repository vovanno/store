using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineStoreApi.ManufactureApi;
using OnlineStoreApi.ProductApi;

namespace WebApi.Controllers
{
    [Route("api/manufacturer")]
    [ApiController]
    public class ManufacturerController : ControllerBase
    {
        private readonly IManufacturerService _manufacturerService;
        private readonly IProductService _productService;

        public ManufacturerController(IManufacturerService manufacturerService, IProductService productService)
        {
            _manufacturerService = manufacturerService;
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ManufacturerResponse>>> GetAll()
        {
            var publishersList = await _manufacturerService.GetAll();
            return Ok(publishersList.Select(p => p.ToManufacturerResponse()));
        }

        [HttpGet("category/{categoryId:int:min(1)}")]
        public async Task<ActionResult<List<ManufacturerResponse>>> GetManufacturersByCategoryId(int categoryId)
        {
            var manufacturers = await _manufacturerService.GetManufacturersByCategory(categoryId);
            return Ok(manufacturers.Select(p => p.ToManufacturerResponse()));
        }

        [HttpGet]
        [Route("{manufacturerId:int:min(1)}")]
        public async Task<ActionResult<ManufacturerResponse>> GetManufacturer(int manufacturerId)
        {
            var result = await _manufacturerService.GetById(manufacturerId);
            return Ok(result.ToManufacturerResponse());
        }

        [HttpPost]
        [AllowAnonymous]
        //[Authorize(Roles = "manager")]
        public async Task<ActionResult> AddManufacturer([FromBody] CreateManufacturerRequest request)
        {
            var publisherModel = request.ToManufacturerModel();

            var createdPublisherId = await _manufacturerService.Create(publisherModel);
            return Ok(createdPublisherId);
        }

        [HttpPut("{manufacturerId:int:min(1)}")]
        //[Authorize(Roles = "publisher")]
        public async Task<ActionResult> EditManufacturer(int manufacturerId, [FromBody] EditManufacturerRequest request)
        {
            var publisherModel = request.ToManufacturerModel();

            await _manufacturerService.Edit(manufacturerId, publisherModel);
            return Ok();
        }

        [HttpDelete]
        [Route("{manufacturerId:int:min(1)}")]
        //[Authorize(Roles = "manager")]
        public async Task<ActionResult> Delete(int manufacturerId)
        {
            await _manufacturerService.Delete(manufacturerId);
            return Ok();
        }

        [HttpGet]
        [Route("{manufacturerId:int:min(1)}/products")]
        [ProducesResponseType(typeof(List<ProductResponse>), 200)]
        public async Task<ActionResult<List<ProductResponse>>> GetProductsByManufacturer(int manufacturerId)
        {
            var games = await _productService.GetProductsByManufacturerId(manufacturerId);
            var response = games.Select(p => p.ToProductResponse());
            return Ok(response);
        }
    }
}