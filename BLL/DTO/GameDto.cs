using System;
using System.Collections.Generic;

namespace BLL.DTO
{
    public class GameDto
    {
        public int GameId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int AmountOfViews { get; set; }
        public DateTime DateOfAdding { get; set; }
        public int PublisherId { get; set; }
        public IList<GenreDto> Genres { get; set; }
    }
}
