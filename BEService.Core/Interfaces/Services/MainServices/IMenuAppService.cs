using BEService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BEService.Core.Interfaces.Services.MainServices
{
    public interface IMenuAppService
    {
        IEnumerable<MenuApp> GetAllMenuApp();
        Task<MenuApp> GetMenuAppById(Guid id);
        Task<bool> AddMenuApp(MenuApp menuApp);
        Task<bool> UpdateMenuApp(Guid id, MenuApp menuApp);
        Task<bool> DeleteMenuApp(Guid id);
    }
}
