using System.Collections.Generic;

namespace Domain.Entities
{
    public class Manufacturer
    {
        public int ManufacturerId { get; set; }
        public string Name { get; set; }
        public IList<Product> Products { get; set; }
    }
}
