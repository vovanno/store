
namespace DAL.Entities
{
    public class GamePlatform
    {
        public int GameId { get; set; }
        public virtual Game Game { get; set; }
        public int PlatformTypeId { get; set; }
        public virtual PlatformType PlatformType { get; set; }
    }
}
