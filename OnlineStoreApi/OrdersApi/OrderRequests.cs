namespace OnlineStoreApi.OrdersApi
{
    public class CreateOrderRequest
    {
        public int[] GameIds { get; set; }
    }

    public class EditOrderRequest
    {
        public int[] GameIds { get; set; }
    }
}
