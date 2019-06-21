using DAL.Interfaces;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Publisher : ISoftDelete
    {
        public int PublisherId { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; } = false;
        public virtual IList<Game> Games { get; set; }
    }
}
