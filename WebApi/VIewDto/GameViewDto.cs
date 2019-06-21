using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.VIewDto
{
    public sealed class GameViewDto
    {
        private int _publisherId;
        private IList<GenreViewDto> _genres;

        public int GameId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PublisherId
        {
            get => _publisherId;
            set
            {
                if (value == 0)
                {
                    _publisherId = 2;
                    return;
                }
                _publisherId = value;
            }
        }
        public int AmountOfViews { get; set; }
        public double Price { get; set; }
        public IList<GenreViewDto> Genres
        {
            get => _genres;
            set
            {
                _genres = new List<GenreViewDto>();
                if (value.Any(p => p.GenreId == 0))
                {
                    _genres.Add(new GenreViewDto() { GenreId = 2 });
                    return;
                }
                _genres = value;
            }
        }
        public DateTime DateOfAdding { get; set; }
    }
}
