using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IOrderRepository
    {
        Task<IList<Order>> GetOrdersHistory();
        Task<Order> GetById(int orderId);
        Task<IList<Order>> GetAll();
        Task<Order> Add(Order order);
        Task Update(int orderId, int[] gamesIds);
        Task Delete(int orderId);
    }
}
