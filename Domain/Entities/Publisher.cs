using System.Collections.Generic;

namespace Domain.Entities
{
    public class Publisher
    {
        public int PublisherId { get; set; }
        public string Name { get; set; }
        public IList<Game> Games { get; set; }
    }
}
