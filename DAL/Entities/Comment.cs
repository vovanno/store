using System;
using DAL.Interfaces;

namespace DAL.Entities
{
    public class Comment : ISoftDelete
    {
        public int CommentId { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public int AmountOfLikes { get; set; }
        public DateTime DateOfAdding { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int GameId { get; set; }
        public virtual Game Game { get; set; }
        public int? ParentCommentId { get; set; }
    }
}
