using BEService.Core.CustomEntities;
using System.Collections.Generic;

namespace BEService.Core.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<MenuAccess> MenuAccesses { get; set; }
    }
}
