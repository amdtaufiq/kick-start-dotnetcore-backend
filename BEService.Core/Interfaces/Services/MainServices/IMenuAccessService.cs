using BEService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BEService.Core.Interfaces.Services.MainServices
{
    public interface IMenuAccessService
    {
        IEnumerable<MenuAccess> GetAllMenuAccess();
        Task<MenuAccess> GetMenuAccessById(Guid id);
        Task<bool> AddMenuAccess(MenuAccess menuAccess);
        Task<bool> UpdateMenuAccess(Guid id, MenuAccess menuAccess);
        Task<bool> DeleteMenuAccess(Guid id);
    }
}
