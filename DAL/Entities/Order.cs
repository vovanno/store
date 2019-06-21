using DAL.Interfaces;
using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Order : ISoftDelete
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public virtual IList<OrderGame> OrdersGames { get; set; }
        public string Status { get; set; }
        public DateTime? ShippedDate { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
