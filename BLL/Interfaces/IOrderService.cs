using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using OnlineStoreApi.OrdersApi;

namespace BLL.Interfaces
{
    public interface IOrderService
    {
        Task<int> AddOrder(string userId, OrderedItem[] orderedItems);
        Task EditOrder(int id, int[] gamesIds);
        Task<(IList<Order>, IList<Product>)> GetAll(string userId);
        Task<Order> GetOrderById(int id);
        Task<(IList<Order>, IList<Product>)> GetOrdersHistory(string userId);
    }
}
