namespace OnlineStoreApi.OrdersApi
{
    public class CreateOrderRequest
    {
        public string UserId { get; set; }
        public OrderedItem[] OrderedItems { get; set; }
    }

    public class EditOrderRequest
    {
        public int[] ProductsIds { get; set; }
    }

    public class OrderedItem
    {
        public int ProductId { get; set; }
        public int AmountOfItems { get; set; }
    }
}
