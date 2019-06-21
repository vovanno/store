namespace DAL.Entities
{
    public class GameGenre
    {
        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }
        public int GameId { get; set; }
        public virtual Game Game { get; set; }
    }
}
