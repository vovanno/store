using OnlineStoreApi.GenresApi;
using System;
using System.Collections.Generic;
using OnlineStoreApi.CommentApi;
using OnlineStoreApi.PublisherApi;

namespace OnlineStoreApi.GameApi
{
    public sealed class GameResponse
    {
        public int GameId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AmountOfViews { get; set; }
        public double Price { get; set; }
        public DateTime DateOfAdding { get; set; }
        public List<GenreResponse> Genres { get; set; }
        public List<CommentResponse> Comments { get; set; }
        public PublisherResponse Publisher { get; set; }
    }
}
