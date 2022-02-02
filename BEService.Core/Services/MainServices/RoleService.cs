using BEService.Core.Entities;
using BEService.Core.Exceptions;
using BEService.Core.Interfaces.Services.MainServices;
using BEService.Core.Interfaces.Unit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BEService.Core.Services.MainServices
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unit;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger _logger;
        public RoleService(
            IUnitOfWork unit,
            ILoggerFactory loggerFactory)
        {
            _unit = unit;
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger("Role");
        }

        public async Task<bool> AddRole(Role role)
        {
            try
            {
                var menuApps = _unit.MenuAppRepository.GetAll();
                List<MenuAccess> menuAccesses = new List<MenuAccess>();
                foreach(var menuApp in menuApps)
                {
                    var menuAccess = new MenuAccess
                    {
                        MenuAppId = menuApp.Id,
                        CreateAccess = false,
                        ReadAccess = false,
                        UpdateAccess = false,
                        DeleteAccess = false
                    };
                    menuAccesses.Add(menuAccess);
                }
                role.MenuAccesses = menuAccesses;
                await _unit.RoleRepository.Add(role);
                await _unit.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                _logger.LogError("Role Add => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        public async Task<bool> DeleteRole(Guid id)
        {
            try
            {
                var data = await _unit.RoleRepository.GetById(id);
                if(data == null)
                {
                    throw new NotFoundException("Role doesn't exist!");
                }
                _unit.RoleRepository.Delete(data);
                await _unit.SaveChangesAsync(true);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("Role Delete => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        public IEnumerable<Role> GetAllRole()
        {
            try
            {
                var datas = _unit.RoleRepository.GetAll();
                
                return datas;
            }
            catch (Exception e)
            {
                _logger.LogError("Role List => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        public async Task<Role> GetRoleById(Guid id)
        {
            try
            {
                var data = await _unit.RoleRepository.GetById(id);
                if (data == null)
                {
                    throw new NotFoundException("Role doesn't exist!");
                }
                return data;
            }
            catch(Exception e)
            {
                _logger.LogError("Role By ID => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        public async Task<bool> UpdateRole(Guid id, Role role)
        {
            try
            {
                var data = await _unit.RoleRepository.GetById(id);
                if (data == null)
                {
                    throw new NotFoundException("Role doesn't exist!");
                }

                //Set value
                data.Name = role.Name;

                _unit.RoleRepository.Update(data);
                await _unit.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("Role Update => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        public async Task<bool> RefreshMenuAccess()
        {
            var roles = _unit.RoleRepository.GetAllRoleDetail().ToList();
            var menuApps = _unit.MenuAppRepository.GetAll();

            foreach(var role in roles)
            {
                var menuAppIds = menuApps.Select(x => x.Id).Except(role.MenuAccesses.Select(x => x.MenuAppId));
                foreach(var menuAppId in menuAppIds)
                {
                    try
                    {
                        var menuAccess = new MenuAccess
                        {
                            RoleId = role.Id,
                            MenuAppId = menuAppId,
                            CreateAccess = false,
                            ReadAccess = false,
                            UpdateAccess = false,
                            DeleteAccess = false,
                        };

                        await _unit.MenuAccessRepository.Add(menuAccess);
                    }
                    catch (Exception e)
                    {
                        _logger.LogError("Refresh menu access => " + e.Message);
                        throw new InternalServerErrorException(e.Message);
                    }
                }
                await _unit.SaveChangesAsync();

            }

            return true;
        }
    }
}
