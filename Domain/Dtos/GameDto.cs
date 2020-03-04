using System;
using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Dtos
{
    public class GameDto
    {
        public int GameId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AmountOfViews { get; set; }
        public double Price { get; set; }
        public DateTime DateOfAdding { get; set; }
        public Publisher Publisher { get; set; }
        public IList<Comment> Comments { get; set; }
        public IList<Genre> GameGenres { get; set; }
        public IList<PlatformType> PlatformTypes { get; set; }
    }
}
