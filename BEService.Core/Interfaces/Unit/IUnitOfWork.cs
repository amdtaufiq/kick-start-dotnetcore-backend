using BEService.Core.Entities;
using BEService.Core.Interfaces.Repositories;
using System;
using System.Threading.Tasks;

namespace BEService.Core.Interfaces.Unit
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IRoleRepository RoleRepository { get; }
        IBaseRepository<MenuApp> MenuAppRepository { get; }
        IBaseRepository<MenuAccess> MenuAccessRepository { get; }
        void SaveChanges();
        Task SaveChangesAsync(bool delete = false);
    }
}
