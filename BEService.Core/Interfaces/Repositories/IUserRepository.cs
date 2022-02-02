using BEService.Core.Entities;
using System;
using System.Threading.Tasks;

namespace BEService.Core.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByToken(string token);
        Task<User> GetDetailUser(Guid id);
    }
}
