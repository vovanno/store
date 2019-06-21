using System;
using System.Collections.Generic;

namespace WebApi.VIewDto
{
    public class OrderViewDto
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public virtual IList<GameViewDto> OrdersGames { get; set; }
        public string Status { get; set; }
        public DateTime? ShippedDate { get; set; }
    }
}
