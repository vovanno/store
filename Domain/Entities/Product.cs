using System.Collections.Generic;

namespace Domain.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }
        public double Price { get; set; }
        public bool Availability { get; set; }
        public int ManufacturerId { get; set; }
        public int CategoryId { get; set; }
        public int AmountOfComments { get; set; }
        public Category Category { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public IList<Comment> Comments { get; set; }
        public IList<Image> Images { get; set; }
        public IList<OrdersProduct> Orders { get; set; }
    }
}
