using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public IList<OrderGame> OrdersGames { get; set; }
    }
}
