using System;
using System.Collections.Generic;

namespace BEService.Core.DTOs
{
    public class RoleResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual ICollection<MenuAccessResponse> MenuAccesses { get; set; }
    }

    public class CreateRoleRequest
    {
        public string Name { get; set; }
    }

    public class UpdateRoleRequest
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
    }
}
