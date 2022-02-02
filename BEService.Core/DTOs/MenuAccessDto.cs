using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace BEService.Core.DTOs
{
    public class MenuAccessResponse
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public Guid MenuAppId { get; set; }
        public bool CreateAccess { get; set; }
        public bool ReadAccess { get; set; }
        public bool UpdateAccess { get; set; }
        public bool DeleteAccess { get; set; }
        public virtual MenuAppResponse MenuApp { get; set; }
    }

    public class CreateMenuAccessRequest
    {
        public Guid RoleId { get; set; }
        public Guid MenuAppId { get; set; }
        public bool CreateAccess { get; set; }
        public bool ReadAccess { get; set; }
        public bool UpdateAccess { get; set; }
        public bool DeleteAccess { get; set; }
    }

    public class UpdateMenuAccessRequest
    {
        public Guid? Id { get; set; }
        public Guid RoleId { get; set; }
        public Guid MenuAppId { get; set; }
        public bool CreateAccess { get; set; }
        public bool ReadAccess { get; set; }
        public bool UpdateAccess { get; set; }
        public bool DeleteAccess { get; set; }
    }
}
