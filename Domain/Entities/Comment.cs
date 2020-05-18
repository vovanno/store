using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Body { get; set; }
        public int ProductRating { get; set; }
        public int AmountOfLikes { get; set; }
        public DateTime DateOfAdding { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string UserName { get; set; }
        public IList<Comment> Children { get; set; }
    }
}
