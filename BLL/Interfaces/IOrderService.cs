using BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IOrderService
    {
        Task<int> AddOrder(OrderDto order);
        Task EditOrder(int id, OrderDto order);
        Task<IList<OrderDto>> GetAll();
        Task<OrderDto> GetOrderById(int id);
        Task<IList<OrderDto>> GetOrdersHistory();
    }
}
