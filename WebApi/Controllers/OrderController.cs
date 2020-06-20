using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreApi.OrdersApi;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orders;

        public OrderController(IOrderService order)
        {
            _orders = order;
        }

        [HttpPost]
        //[Authorize(Roles = "user, manager, admin, moderator")]
        public async Task<IActionResult> AddOrder([FromBody] CreateOrderRequest request)
        {
            var userId = Request.Headers.GetEmailFromDecodedToken();
            var orderId = await _orders.AddOrder(userId, request.OrderedItems);

            if (orderId == 0)
                return BadRequest("You can not make an order for a non-existent game");

            return Ok();
        }

        [HttpPut]
        [Route("{orderId:int:min(1)}")]
        //[Authorize(Roles = "manager")]
        public async Task<IActionResult> EditOrder(int orderId, [FromBody] EditOrderRequest request)
        {
            await _orders.EditOrder(orderId, request.ProductsIds);
            return NoContent();
        }

        [HttpPost("All")]
        //[Authorize(Roles = "manager")]
        public async Task<ActionResult<List<OrderResponse>>> GetAllOrders()
        {
            var userId = Request.Headers.GetEmailFromDecodedToken();

            var (orders, products) = await _orders.GetAll(userId);
            var ordersResponse = orders.Select(p => new OrderResponse
            {
                OrderId = p.OrderId,
                OrderDate = p.OrderDate,
                Status = p.Status,
                OrderedProducts = products.Where(x => p.Products.Select(y => y.ProductId).Contains(x.ProductId)).Select(
                    z => z.ToProductResponse(p.Products)).ToList()
            }).ToList();

            return Ok(ordersResponse);
        }

        [HttpGet]
        [Route("history")]
        //[Authorize(Roles = "manager")]
        public async Task<ActionResult<List<OrderResponse>>> GetOrdersHistory()
        {
            var userId = Request.Headers.GetEmailFromDecodedToken();
            var (orders, products) = await _orders.GetOrdersHistory(userId);
            var ordersResponse = orders.Select(p => new OrderResponse
            {
                OrderId = p.OrderId,
                OrderDate = p.OrderDate,
                Status = p.Status,
                OrderedProducts = products.Where(x => p.Products.Select(y => y.ProductId).Contains(x.ProductId)).Select(
                    z => z.ToProductResponse(p.Products)).ToList()
            }).ToList();

            return Ok(ordersResponse);
        }

        [HttpGet]
        [Route("{id:int:min(1)}")]
        //[Authorize(Roles = "user, manager")]
        public async Task<ActionResult<OrderResponse>> GetOrderById(int id)
        {
            var order = await _orders.GetOrderById(id);
            return Ok(order.ToOrderResponse());
        }
    }
}