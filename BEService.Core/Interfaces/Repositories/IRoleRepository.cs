using BEService.Core.Entities;
using System.Collections.Generic;

namespace BEService.Core.Interfaces.Repositories
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        IEnumerable<Role> GetAllRoleDetail();
    }
}
