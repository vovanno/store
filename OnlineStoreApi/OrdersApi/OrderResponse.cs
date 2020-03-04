using System;
using System.Collections.Generic;
using OnlineStoreApi.GameApi;

namespace OnlineStoreApi.OrdersApi
{
    public class OrderResponse
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public virtual List<GameResponse> OrderedGames { get; set; }
        public string Status { get; set; }
        public DateTime? ShippedDate { get; set; }
    }
}
