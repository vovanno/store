using DAL.Interfaces;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Genre : ISoftDelete
    {
        public int GenreId { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public virtual IList<SubGenre> SubGenres { get; set; }
        public virtual IList<GameGenre> GameGenres { get; set; }

        public class SubGenre : ISoftDelete
        {
            public int SubGenreId { get; set; }
            public string Name { get; set; }
            public bool IsDeleted { get; set; } = false;
            public int GenreId { get; set; }
            public virtual Genre Genre { get; set; }
        }
    }
}
