using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unit, IMapper mapper)
        {
            _mapper = mapper;
            _unit = unit;
        }

        public async Task<int> AddOrder(OrderDto order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            //Create order without games, in order to receive orderId.
            var orderItems = order.OrdersGames;
            order.OrdersGames = null;
            var tempOrder = _mapper.Map<Order>(order);
            var result = await _unit.OrderRepository.Add(tempOrder);
            await _unit.Commit();

            //Try add games to the created order, and update order entity.
            var orderGames = orderItems.Select(item => new OrderGame() { OrderId = result.OrderId, GameId = item.GameId }).ToList();
            result.OrdersGames = orderGames;
            await _unit.OrderRepository.Update(result.OrderId, result);
            try
            {
                await _unit.Commit();
            }
            catch (Exception)
            {
                await _unit.OrderRepository.PermanentDelete(result);
                await _unit.Commit();
                return 0;
            }
            return result.OrderId;
        }

        public async Task EditOrder(int id, OrderDto order)
        {
            if (id < 0)
                throw new ArgumentException("Id can not be less than 0");
            if (order == null)
                throw new ArgumentNullException(nameof(order));
            var tempOrder = _mapper.Map<Order>(order);
            await _unit.OrderRepository.Update(id, tempOrder);
            await _unit.Commit();
        }

        public async Task<IList<OrderDto>> GetAll()
        {
            var result = await _unit.OrderRepository.GetAll();
            return _mapper.Map<IList<OrderDto>>(result);
        }

        public async Task<IList<OrderDto>> GetOrdersHistory()
        {
            var result = await _unit.OrderRepository.GetOrdersHistory();
            return _mapper.Map<IList<OrderDto>>(result);
        }

        public async Task<OrderDto> GetOrderById(int id)
        {
            if (id < 0)
                throw new ArgumentException("Id can not be less than 0");
            var tempOrder = await _unit.OrderRepository.GetById(id);
            return _mapper.Map<OrderDto>(tempOrder);
        }
    }
}
