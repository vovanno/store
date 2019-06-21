using System.Collections.Generic;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using WebApi.VIewDto;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orders;
        private readonly IMapper _mapper;

        public OrderController(IOrderService order, IMapper mapper)
        {
            _mapper = mapper;
            _orders = order;
        }

        [HttpPost]
        [Authorize(Roles = "user, manager, admin, moderator")]
        public async Task<IActionResult> AddOrder([FromBody] OrderViewDto order)
        {
            var tempOrder = _mapper.Map<OrderDto>(order);
            var result = await _orders.AddOrder(tempOrder);
            if (result == 0)
                return BadRequest("You can not make an order for a non-existent game");
            return Ok(result);
        }

        [HttpPut]
        [Route("{id:int:min(1)}")]
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> EditOrder(int id, [FromBody] OrderViewDto order)
        {
            var tempOrder = _mapper.Map<OrderDto>(order);
            await _orders.EditOrder(id, tempOrder);
            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> GetAllOrders()
        {
            var result = await _orders.GetAll();
            var orders = _mapper.Map<IList<OrderViewDto>>(result);
            return Ok(orders);
        }

        [HttpGet]
        [Route("history")]
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> GetOrdersHistory()
        {
            var result = await _orders.GetOrdersHistory();
            var orders = _mapper.Map<IList<OrderViewDto>>(result);
            return Ok(orders);
        }

        [HttpGet]
        [Route("{id:int:min(1)}")]
        [Authorize(Roles = "user, manager")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var result = await _orders.GetOrderById(id);
            var order = _mapper.Map<OrderViewDto>(result);
            return Ok(order);
        }
    }
}