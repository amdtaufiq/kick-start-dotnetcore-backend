using BEService.Core.CustomEntities;
using System;

namespace BEService.Core.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public Guid RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
