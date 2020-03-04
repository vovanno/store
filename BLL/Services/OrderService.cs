using BLL.Interfaces;
using DAL.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unit;

        public OrderService(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task<int> AddOrder(int[] gamesIds)
        {
            var utcNow = DateTime.UtcNow;
            var orderRequest = new Order { OrderDate = utcNow };

            //Create order without games, in order to receive orderId.
            var order = await _unit.OrderRepository.Add(orderRequest);
            await _unit.Commit();

            //Try add games to the created order, and update order entity.
            await _unit.OrderRepository.Update(order.OrderId, gamesIds);
            try
            {
                await _unit.Commit();
            }
            catch (Exception)
            {
                await _unit.OrderRepository.Delete(order.OrderId);
                await _unit.Commit();
                return 0;
            }
            return order.OrderId;
        }

        public async Task EditOrder(int id, int[] gamesIds)
        {
            if (!gamesIds.Any())
                return;

            await _unit.OrderRepository.Update(id, gamesIds);  
            await _unit.Commit();
        }

        public async Task<IList<Order>> GetAll()
        {
            var result = await _unit.OrderRepository.GetAll();
            return result.ToList();
        }

        public async Task<IList<Order>> GetOrdersHistory()
        {
            var result = await _unit.OrderRepository.GetOrdersHistory();
            return result.ToList();
        }

        public async Task<Order> GetOrderById(int id)
        {
            if (id < 0)
                throw new ArgumentException("Id can not be less than 0");
            var tempOrder = await _unit.OrderRepository.GetById(id);
            return tempOrder;
        }
    }
}
