using BEService.Core.Entities;
using BEService.Core.Interfaces.Repositories;
using BEService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BEService.Infrastructure.Repositories
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {

        public RoleRepository(BEDBContext context) : base(context)
        {

        }

        public IEnumerable<Role> GetAllRoleDetail()
        {
            return _ctx.Roles
                .Include(x => x.MenuAccesses.Where(x => x.IsDelete == false))
                .ThenInclude(x => x.MenuApp)
                .Where(x => x.IsDelete == false)
                .AsEnumerable();
                
        }
    }
}
