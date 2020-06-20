namespace OnlineStoreApi.ProductApi
{
    public class CreateProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool Availability { get; set; }
        public int ManufacturerId { get; set; }
        public int CategoryId { get; set; }
    }

    public class GetProductsByIds
    {
        public int[] ProductsIds { get; set; }
    }

    public class EditProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool Availability { get; set; }
        public int ManufacturerId { get; set; }
        public int CategoryId { get; set; }
    }

    public class UploadImageRequest
    {
        public CreateImageRequest[] CreateImagesRequest { get; set; }
    }

    public class CreateImageRequest
    {
        public string ImageContent { get; set; }
        public bool IsMain { get; set; }
    }
}
