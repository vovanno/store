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

        public async Task<int> AddOrder(int[] productIds)
        {
            var foundProducts = await CheckProductsAvailability(productIds);
            var utcNow = DateTime.UtcNow;

            var orderRequest = new Order { OrderDate = utcNow };
            var order = await _unit.OrderRepository.Add(orderRequest);

            var products = foundProducts.Select(p => new OrdersProduct { OrderId = order.OrderId, ProductId = p.ProductId }).ToList();
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
