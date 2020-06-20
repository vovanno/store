using BLL.Interfaces;
using DAL.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OnlineStoreApi.OrdersApi;
using OnlineStoreApi.ProductApi;

namespace BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unit;
        private readonly UserManager<IdentityUser> _userManager;

        public OrderService(IUnitOfWork unit, UserManager<IdentityUser> userManager)
        {
            _unit = unit;
            _userManager = userManager;
        }

        public async Task<int> AddOrder(string userId, OrderedItem[] orderedItems)
        {
            var foundProducts = await CheckProductsAvailability(orderedItems.Select(p => p.ProductId).ToArray());
            var utcNow = DateTime.UtcNow;

            var existedUser = await _userManager.FindByEmailAsync(userId);
            if(existedUser == null)
                throw new Exception($"User does not exist");

            var orderRequest = new Order {UserId = userId, OrderDate = utcNow };
            var order = await _unit.OrderRepository.Add(orderRequest);

            var products = foundProducts.Select(p => new OrdersProduct
            {
                OrderId = order.OrderId,
                ProductId = p.ProductId,
                AmountOfItems = orderedItems.Single(x => x.ProductId == p.ProductId).AmountOfItems
            }).ToList();
            order.Products = products;

            await _unit.Commit();

            return order.OrderId;
        }

        public async Task EditOrder(int orderId, int[] productsIds)
        {
            var products = await CheckProductsAvailability(productsIds);

            await _unit.OrderRepository.Update(orderId, products);
            await _unit.Commit();
        }

        public async Task<(IList<Order>, IList<Product>)> GetAll(string userId)
        {
            var orders = await _unit.OrderRepository.GetAll(userId);
            var products = await _unit.ProductRepository.GetProductsByIds(orders.SelectMany(p =>
                p.Products.Select(x => x.ProductId)).ToArray());
            return (orders, products);
        }

        public async Task<(IList<Order>, IList<Product>)> GetOrdersHistory(string userId)
        {
            var orders = await _unit.OrderRepository.GetOrdersHistory(userId);
            var products = await _unit.ProductRepository.GetProductsByIds(orders.SelectMany(p =>
                p.Products.Select(x => x.ProductId)).ToArray());

            return (orders, products);
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            var tempOrder = await _unit.OrderRepository.GetById(orderId);
            return tempOrder;
        }

        private async Task<List<Product>> CheckProductsAvailability(int[] productIds)
        {
            var foundProducts = await _unit.ProductRepository.GetProductsByIds(productIds);

            if (foundProducts.Count != productIds.Length)
                throw new ArgumentException(nameof(productIds));

            foreach (var product in foundProducts)
            {
                if (!product.Availability)
                    throw new ArgumentException(nameof(productIds));
            }

            return foundProducts;
        }
    }
}
