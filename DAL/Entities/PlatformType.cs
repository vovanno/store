using DAL.Interfaces;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class PlatformType : ISoftDelete
    {
        public int PlatformTypeId { get; set; }
        public string Type { get; set; }
        public bool IsDeleted { get; set; } = false;
        public virtual IList<GamePlatform> Games { get; set; }
    }
}
