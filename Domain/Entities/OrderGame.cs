namespace Domain.Entities
{
    public class OrderGame
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
    }
}
