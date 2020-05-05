using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreApi.OrdersApi;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<ActionResult<int>> AddOrder([FromBody] CreateOrderRequest request)
        {
            var orderedProductsIds = request.ProductsIds;
            var orderId = await _orders.AddOrder(orderedProductsIds);

            if (orderId == 0)
                return BadRequest("You can not make an order for a non-existent game");

            return Ok(orderId);
        }

        [HttpPut]
        [Route("{orderId:int:min(1)}")]
        //[Authorize(Roles = "manager")]
        public async Task<IActionResult> EditOrder(int orderId, [FromBody] EditOrderRequest request)
        {
            await _orders.EditOrder(orderId, request.ProductsIds);
            return NoContent();
        }

        [HttpGet]
        //[Authorize(Roles = "manager")]
        public async Task<ActionResult<List<OrderResponse>>> GetAllOrders()
        {
            var orders = await _orders.GetAll();
            return Ok(orders.ToOrderResponse());
        }

        [HttpGet]
        [Route("history")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<List<OrderResponse>>> GetOrdersHistory()
        {
            var ordersHistory = await _orders.GetOrdersHistory();
            return Ok(ordersHistory.ToOrderResponse());
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