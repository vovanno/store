using System.Collections.Generic;

namespace Domain.Entities
{
    public class PlatformType
    {
        public int PlatformTypeId { get; set; }
        public string Type { get; set; }
        public IList<GamePlatform> Games { get; set; }
    }
}
