using System;

namespace BLL.DTO
{
    public class CommentDto
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
