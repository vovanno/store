using System;
using System.Collections.Generic;
using OnlineStoreApi.ProductApi;

namespace OnlineStoreApi.OrdersApi
{
    public class OrderResponse
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public virtual List<ProductResponse> OrderedProducts { get; set; }
        public string Status { get; set; }
    }
}
