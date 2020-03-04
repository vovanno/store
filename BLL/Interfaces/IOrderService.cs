using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace BLL.Interfaces
{
    public interface IOrderService
    {
        Task<int> AddOrder(int[] gamesIds);
        Task EditOrder(int id, int[] gamesIds);
        Task<IList<Order>> GetAll();
        Task<Order> GetOrderById(int id);
        Task<IList<Order>> GetOrdersHistory();
    }
}
