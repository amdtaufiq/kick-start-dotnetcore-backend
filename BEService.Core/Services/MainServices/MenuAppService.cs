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
    public class MenuAppService : IMenuAppService
    {
        private readonly IUnitOfWork _unit;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger _logger;
        public MenuAppService(
            IUnitOfWork unit,
            ILoggerFactory loggerFactory)
        {
            _unit = unit;
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger("MenuApp");
        }

        public async Task<bool> AddMenuApp(MenuApp role)
        {
            try
            {
                await _unit.MenuAppRepository.Add(role);
                await _unit.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("MenuApp Add => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        public async Task<bool> DeleteMenuApp(Guid id)
        {
            try
            {
                var data = await _unit.MenuAppRepository.GetById(id);
                if (data == null)
                {
                    throw new NotFoundException("MenuApp doesn't exist!");
                }
                _unit.MenuAppRepository.Delete(data);
                await _unit.SaveChangesAsync(true);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("MenuApp Delete => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        public IEnumerable<MenuApp> GetAllMenuApp()
        {
            try
            {
                var datas = _unit.MenuAppRepository.GetAll();

                return datas;
            }
            catch (Exception e)
            {
                _logger.LogError("MenuApp List => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        public async Task<MenuApp> GetMenuAppById(Guid id)
        {
            try
            {
                var data = await _unit.MenuAppRepository.GetById(id);
                if (data == null)
                {
                    throw new NotFoundException("MenuApp doesn't exist!");
                }
                return data;
            }
            catch (Exception e)
            {
                _logger.LogError("MenuApp By ID => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        public async Task<bool> UpdateMenuApp(Guid id, MenuApp role)
        {
            try
            {
                var data = await _unit.MenuAppRepository.GetById(id);
                if (data == null)
                {
                    throw new NotFoundException("MenuApp doesn't exist!");
                }

                //Set value
                data.MenuLabel = role.MenuLabel;
                data.MenuUrl = role.MenuUrl;
                data.Ordering = role.Ordering;

                _unit.MenuAppRepository.Update(data);
                await _unit.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("MenuApp Update => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }
    }
}
