using BEService.Core.Entities;
using BEService.Core.Interfaces.Repositories;
using BEService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BEService.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(BEDBContext context): base(context)
        {

        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _ctx.Users
                .Include(x => x.Role)
                .Where(x => x.Email == email && 
                            x.IsDelete == false)
                .FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByToken(string token)
        {
            return await _ctx.Users
                .Include(x => x.Role)
                .ThenInclude(x => x.MenuAccesses)
                .ThenInclude(x => x.MenuApp)
                .Where(x => x.Token == token &&
                            x.IsDelete == false)
                .FirstOrDefaultAsync();
        }

        public async Task<User> GetDetailUser(Guid id)
        {
            return await _ctx.Users
                .Include(x => x.Role)
                .Where(x => x.IsDelete == false &&
                            x.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
