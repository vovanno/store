using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    internal class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(IAppContext context) : base(context)
        {

        }

        public async Task PermanentDelete(Order order)
        {
            await Task.Run(() => Db.Remove(order));
        }

        public new async Task<IEnumerable<Order>> GetAll()
        {
            return await Db.Where(p => (DateTime.Now - p.OrderDate).Days < 30).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersHistory()
        {
            return await Db.Where(p => (DateTime.Now - p.OrderDate).Days > 30).ToListAsync();
        }
    }
}
