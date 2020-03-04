using System.Collections.Generic;

namespace Domain.Entities
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string Name { get; set; }
        public IList<SubGenre> SubGenres { get; set; }
        public IList<GameGenre> GameGenres { get; set; }

        public class SubGenre
        {
            public int SubGenreId { get; set; }
            public string Name { get; set; }
            public int GenreId { get; set; }
            public Genre Genre { get; set; }
        }
    }
}
