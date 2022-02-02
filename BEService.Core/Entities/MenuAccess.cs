using BEService.Core.CustomEntities;
using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace BEService.Core.Entities
{
    public class MenuAccess : BaseEntity
    {
        public Guid RoleId { get; set; }
        public Guid MenuAppId { get; set; }
        public bool CreateAccess { get; set; }
        public bool ReadAccess { get; set; }
        public bool UpdateAccess { get; set; }
        public bool DeleteAccess { get; set; }
        public virtual MenuApp MenuApp { get; set; }
    }
}
