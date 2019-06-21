﻿namespace DAL.Entities
{
    public class OrderGame
    {
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int GameId { get; set; }
        public virtual Game Game { get; set; }
    }
}
