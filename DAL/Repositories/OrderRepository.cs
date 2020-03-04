using DAL.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Exceptions;
using MySql.Data.MySqlClient;

namespace DAL.Repositories
{
    internal class OrderRepository : IOrderRepository
    {
        private readonly Context.AppContext _context;

        public OrderRepository(Context.AppContext context)
        {
            _context = context;
        }

        public async Task<Order> GetById(int orderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(p => p.OrderId == orderId);
            return order ?? throw new EntryNotFoundException($"Order with OrderId={orderId} was not found");
        }

        public async Task<IList<Order>> GetAll()
        {
            return await _context.Orders.AsNoTracking().Where(p => (DateTime.Now - p.OrderDate).Days < 30).ToListAsync();
        }

        public async Task<Order> Add(Order order)
        {
            var createdOrder = await _context.Orders.AddAsync(order);
            return createdOrder.Entity;
        }

        public async Task Update(int orderId, int[] gamesIds)
        {
            var foundOrder = await _context.Orders.FirstOrDefaultAsync(p => p.OrderId == orderId);

            if(foundOrder == null)
                throw new EntryNotFoundException($"Order with OrderId={orderId} was not found");

            var orderGames = gamesIds.Select(p => new OrderGame {GameId = p, OrderId = orderId}).ToList();
            foundOrder.OrdersGames = orderGames;

            _context.Orders.Update(foundOrder);
        }

        public async Task Delete(int orderId)
        {
            await _context.Database.ExecuteSqlCommandAsync("DELETE FROM Orders WHERE OrderId=@orderId",
                new MySqlParameter("@orderId", orderId));
        }

        public async Task<IList<Order>> GetOrdersHistory()
        {
            return await _context.Orders.Where(p => (DateTime.Now - p.OrderDate).Days > 30).ToListAsync();
        }
    }
}
