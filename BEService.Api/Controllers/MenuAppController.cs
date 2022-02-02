using AutoMapper;
using BEService.Core.CustomEntities;
using BEService.Core.DTOs;
using BEService.Core.Entities;
using BEService.Core.Interfaces.Services.MainServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BEService.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MenuAppController : ControllerBase
    {
        private readonly IMenuAppService _menuAppService;
        private readonly IMapper _mapper;

        public MenuAppController(
            IMenuAppService menuAppService,
            IMapper mapper)
        {
            _menuAppService = menuAppService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllMenuApp()
        {
            var menuApps = _menuAppService.GetAllMenuApp();
            var menuAppDtos = _mapper.Map<IEnumerable<MenuAppResponse>>(menuApps);

            var response = new ApiResponse<IEnumerable<MenuAppResponse>>(menuAppDtos)
            {
                Message = new Message
                {
                    Description = "List data Menu App"
                }
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenuAppByID(Guid id)
        {
            var menuApp = await _menuAppService.GetMenuAppById(id);
            var menuAppDto = _mapper.Map<MenuAppResponse>(menuApp);

            var response = new ApiResponse<MenuAppResponse>(menuAppDto)
            {
                Message = new Message
                {
                    Description = "Detail data Menu App"
                }
            };

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateMenuApp(CreateMenuAppRequest request)
        {
            var menuAppMap = _mapper.Map<MenuApp>(request);
            var result = await _menuAppService.AddMenuApp(menuAppMap);

            var response = new ApiResponse<bool>(result)
            {
                Message = new Message
                {
                    Description = "Success create Menu App"

                }
            };

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMenuApp(Guid id, UpdateMenuAppRequest request)
        {
            var menuAppMap = _mapper.Map<MenuApp>(request);
            var result = await _menuAppService.UpdateMenuApp(id, menuAppMap);

            var response = new ApiResponse<bool>(result)
            {
                Message = new Message
                {
                    Description = "Success update Menu App"

                }
            };

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenuApp(Guid id)
        {
            var result = await _menuAppService.DeleteMenuApp(id);

            var response = new ApiResponse<bool>(result)
            {
                Message = new Message
                {
                    Description = "Success delete Menu App"
                }
            };

            return Ok(response);
        }
    }
}
