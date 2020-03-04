using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Game
    {
        public int GameId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AmountOfViews { get; set; }
        public double Price { get; set; }
        public DateTime DateOfAdding { get; set; }
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }
        public IList<Comment> Comments { get; set; }
        public IList<GameGenre> GameGenres { get; set; }
        public IList<GamePlatform> PlatformTypes { get; set; }
        public IList<OrderGame> OrderGames { get; set; }
    }
}
