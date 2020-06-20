using System.Collections.Generic;
using OnlineStoreApi.CategoryApi;
using OnlineStoreApi.CommentApi;
using OnlineStoreApi.ManufactureApi;

namespace OnlineStoreApi.ProductApi
{
    public sealed class ProductResponse
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }
        public double Price { get; set; }
        public bool Availability { get; set; }
        public CategoryResponse Category { get; set; }
        public ManufacturerResponse Manufacturer { get; set; }
        public IList<ImageResponse> Images { get; set; }
        public int AmountOfItems { get; set; }
    }

    public class ImageResponse
    {
        public string ImageUrl { get; set; }
    }
}
