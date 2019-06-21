using System;
using DAL.Interfaces;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Game : ISoftDelete
    {
        public int GameId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AmountOfViews { get; set; }
        public double Price { get; set; }
        public DateTime DateOfAdding { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int PublisherId { get; set; }
        public virtual Publisher Publisher { get; set; }
        public virtual IList<Comment> Comments { get; set; }
        public virtual IList<GameGenre> GameGenres { get; set; }
        public virtual IList<GamePlatform> PlatformTypes { get; set; }
        public virtual IList<OrderGame> OrderGames { get; set; }
    }
}
