using BEService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BEService.Core.Interfaces.Services.MainServices
{
    public interface IRoleService
    {
        IEnumerable<Role> GetAllRole();
        Task<Role> GetRoleById(Guid id);
        Task<bool> AddRole(Role role);
        Task<bool> UpdateRole(Guid id, Role role);
        Task<bool> DeleteRole(Guid id);
        Task<bool> RefreshMenuAccess();
    }
}
