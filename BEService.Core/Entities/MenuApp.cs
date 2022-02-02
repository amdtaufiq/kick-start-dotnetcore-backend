using BEService.Core.CustomEntities;

namespace BEService.Core.Entities
{
    public class MenuApp : BaseEntity
    {
        public string MenuLabel { get; set; }
        public string MenuUrl { get; set; }
        public int Ordering { get; set; }
    }
}
