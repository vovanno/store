using System;

namespace WebApi.VIewDto
{
    public class CommentViewDto
    {
        public int CommentId { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public int AmountOfLikes { get; set; }
        public DateTime DateOfAdding { get; set; }
        public int GameId { get; set; }
        public int? ParentId { get; set; }
    }
}
