using System;

namespace Domain.Entities.FilterModels
{
    public class FilterModel : PagingModel
    {
        public int[] ManufacturersIds { get; set; }
        public int CategoryId { get; set; }
        public bool IsMostPopular { get; set; } = false;
        public bool IsMostCommented { get; set; } = false;
        public bool ByPriceAscending { get; set; } = false;
        public bool ByPriceDescending { get; set; } = false;
        public bool ByDateDescending { get; set; } = false;
        public bool ByDateAscending { get; set; } = false;
        public PriceFilter PriceFilter { get; set; }
        //public DateTime DateOfAdding { get; set; } = DateTime.ParseExact("2000-01-01 12:00", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
        public Uri NextPage { get; set; }
        public Uri PreviousPage { get; set; }
    }
}
