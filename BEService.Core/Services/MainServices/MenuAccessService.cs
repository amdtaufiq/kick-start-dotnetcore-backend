using BEService.Core.Entities;
using BEService.Core.Exceptions;
using BEService.Core.Interfaces.Services.MainServices;
using BEService.Core.Interfaces.Unit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BEService.Core.Services.MainServices
{
    public class MenuAccessService : IMenuAccessService
    {
        private readonly IUnitOfWork _unit;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger _logger;
        public MenuAccessService(
            IUnitOfWork unit,
            ILoggerFactory loggerFactory)
        {
            _unit = unit;
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger("MenuAccess");
        }

        public async Task<bool> AddMenuAccess(MenuAccess menuAccess)
        {
            try
            {
                await _unit.MenuAccessRepository.Add(menuAccess);
                await _unit.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("MenuAccess Add => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        public async Task<bool> DeleteMenuAccess(Guid id)
        {
            try
            {
                var data = await _unit.MenuAccessRepository.GetById(id);
                if (data == null)
                {
                    throw new NotFoundException("MenuAccess doesn't exist!");
                }
                _unit.MenuAccessRepository.Delete(data);
                await _unit.SaveChangesAsync(true);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("MenuAccess Delete => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        public IEnumerable<MenuAccess> GetAllMenuAccess()
        {
            try
            {
                var datas = _unit.MenuAccessRepository.GetAll();

                return datas;
            }
            catch (Exception e)
            {
                _logger.LogError("MenuAccess List => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        public async Task<MenuAccess> GetMenuAccessById(Guid id)
        {
            try
            {
                var data = await _unit.MenuAccessRepository.GetById(id);
                if (data == null)
                {
                    throw new NotFoundException("MenuAccess doesn't exist!");
                }
                return data;
            }
            catch (Exception e)
            {
                _logger.LogError("MenuAccess By ID => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        public async Task<bool> UpdateMenuAccess(Guid id, MenuAccess menuAccess)
        {
            try
            {
                var data = await _unit.MenuAccessRepository.GetById(id);
                if (data == null)
                {
                    throw new NotFoundException("MenuAccess doesn't exist!");
                }

                //Set value
                data.CreateAccess = menuAccess.CreateAccess;
                data.ReadAccess = menuAccess.ReadAccess;
                data.UpdateAccess = menuAccess.UpdateAccess;
                data.DeleteAccess = menuAccess.DeleteAccess;

                _unit.MenuAccessRepository.Update(data);
                await _unit.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("MenuAccess Update => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }
    }
}
