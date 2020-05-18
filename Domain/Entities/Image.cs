namespace Domain.Entities
{
    public class Image
    {
        public int ImageId { get; set; }
        public string ImageKey { get; set; }
        public bool IsMain { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
