using System;
using System.Collections.Generic;

namespace OnlineStoreApi.CommentApi
{
    public class CommentResponse
    {
        public int CommentId { get; set; }
        public string Body { get; set; }
        public int ProductRating { get; set; }
        public int AmountOfLikes { get; set; }
        public DateTime DateOfAdding { get; set; }
        public int ProductId { get; set; }
        public List<CommentResponse> Children { get; set; }
    }
}
