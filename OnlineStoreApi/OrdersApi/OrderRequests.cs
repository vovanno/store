namespace OnlineStoreApi.OrdersApi
{
    public class CreateOrderRequest
    {
        public int[] ProductsIds { get; set; }
    }

    public class EditOrderRequest
    {
        public int[] ProductsIds { get; set; }
    }
}
