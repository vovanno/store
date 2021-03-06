﻿using DAL.Interfaces;
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
        private readonly Context.StoreContext _context;

        public OrderRepository(Context.StoreContext context)
        {
            _context = context;
        }

        public async Task<Order> GetById(int orderId)
        {
            var order = await _context.Orders.Where(p => p.OrderId == orderId)
                .Include(p => p.Products)
                    .ThenInclude(p => p.Product)
                        .ThenInclude(p => p.Manufacturer)
                .Include(p => p.Products)
                    .ThenInclude(p => p.Product)
                        .ThenInclude(p => p.Category)
                .Include(p => p.Products)
                    .ThenInclude(p => p.Product)
                        .ThenInclude(p => p.Images)
                .FirstOrDefaultAsync();

            return order ?? throw new EntryNotFoundException($"Order with OrderId={orderId} was not found");
        }

        public async Task<IList<Order>> GetAll(string userId)
        {
            return await _context.Orders
                .Include(p => p.Products)
                .AsNoTracking().Where(p => p.UserId == userId && (DateTime.Now - p.OrderDate).Days < 30).ToListAsync();
        }

        public async Task<Order> Add(Order order)
        {
            var createdOrder = await _context.Orders.AddAsync(order);
            return createdOrder.Entity;
        }

        public async Task Update(int orderId, List<Product> products)
        {
            var foundOrder = await _context.Orders.FirstOrDefaultAsync(p => p.OrderId == orderId);

            if(foundOrder == null)
                throw new EntryNotFoundException($"Order with OrderId={orderId} was not found");

            //foundOrder.Products = products;
        }

        public async Task Delete(int orderId)
        {
            await _context.Database.ExecuteSqlCommandAsync("DELETE FROM Orders WHERE OrderId=@orderId",
                new MySqlParameter("@orderId", orderId));
        }

        public async Task<IList<Order>> GetOrdersHistory(string userId)
        {
            return await _context.Orders.Include(p => p.Products).Where(p => p.UserId == userId).ToListAsync();
        }
    }
}
