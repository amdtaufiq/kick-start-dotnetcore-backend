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
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public RoleController(
            IRoleService roleService,
            IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllRole()
        {
            var roles = _roleService.GetAllRole();
            var roleDtos = _mapper.Map<IEnumerable<RoleResponse>>(roles);

            var response = new ApiResponse<IEnumerable<RoleResponse>>(roleDtos)
            {
                Message = new Message
                {
                    Description = "List data role"
                }
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleByID(Guid id)
        {
            var role = await _roleService.GetRoleById(id);
            var roleDto = _mapper.Map<RoleResponse>(role);

            var response = new ApiResponse<RoleResponse>(roleDto)
            {
                Message = new Message
                {
                    Description = "Detail data role"
                }
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleRequest request)
        {
            var roleMap = _mapper.Map<Role>(request);
            var result = await _roleService.AddRole(roleMap);

            var response = new ApiResponse<bool>(result)
            {
                Message = new Message
                {
                    Description = "Success create role"

                }
            };

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(Guid id, UpdateRoleRequest request)
        {
            var roleMap = _mapper.Map<Role>(request);
            var result = await _roleService.UpdateRole(id, roleMap);

            var response = new ApiResponse<bool>(result)
            {
                Message = new Message
                {
                    Description = "Success update role"

                }
            };

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            var result = await _roleService.DeleteRole(id);

            var response = new ApiResponse<bool>(result)
            {
                Message = new Message
                {
                    Description = "Success delete role"
                }
            };

            return Ok(response);
        }

        [HttpGet("refresh-menuaccess")]
        public async Task<IActionResult> RefreshMenuAccess()
        {
            var result = await _roleService.RefreshMenuAccess();
            var response = new ApiResponse<bool>(result)
            {
                Message = new Message
                {
                    Description = "Success refresh menu access"
                }
            };

            return Ok(response);
        }
    }
}
