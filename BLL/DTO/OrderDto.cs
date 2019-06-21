using System;
using System.Collections.Generic;

namespace BLL.DTO
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public virtual IList<GameDto> OrdersGames { get; set; }
        public string Status { get; set; }
        public DateTime? ShippedDate { get; set; }
    }
}
