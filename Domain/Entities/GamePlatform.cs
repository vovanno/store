namespace Domain.Entities
{
    public class GamePlatform
    {
        public int GameId { get; set; }
        public Game Game { get; set; }
        public int PlatformTypeId { get; set; }
        public PlatformType PlatformType { get; set; }
    }
}
