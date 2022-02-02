using AutoMapper;
using BEService.Core.CustomEntities;
using BEService.Core.DTOs;
using BEService.Core.Entities;
using BEService.Core.Filters;
using BEService.Core.Interfaces.Services.MainServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BEService.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(
            IUserService userService,
            IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllUser([FromQuery] UserFilter filter)
        {
            var users = _userService.GetAllUser(filter);
            var metaData = new Metadata()
            {
                TotalCount = users.TotalCount,
                PageSize = users.PageSize,
                CurrentPage = users.CurrentPage,
                TotalPages = users.TotalPages,
                HasNextPage = users.HasNextPage,
                HasPreviousPage = users.HasPreviousPage
            };
            var userDtos = _mapper.Map<IEnumerable<UserResponse>>(users);

            var response = new ApiResponse<IEnumerable<UserResponse>>(userDtos)
            {
                Message = new Message
                {
                    Description = "list data user"
                },
                Meta = metaData
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metaData));

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByID(Guid id)
        {
            var user = await _userService.GetUserById(id);
            var userDto = _mapper.Map<UserResponse>(user);

            var response = new ApiResponse<UserResponse>(userDto)
            {
                Message = new Message
                {
                    Description = "Detail data user"
                }
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserRequest request)
        {
            var userMap = _mapper.Map<User>(request);
            var result = await _userService.AddUser(userMap);

            var response = new ApiResponse<bool>(result)
            {
                Message = new Message
                {
                    Description = "Success create user"
                }
            };

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, UpdateUserRequest request)
        {
            var userMap = _mapper.Map<User>(request);
            var result = await _userService.UpdateUser(id, userMap);

            var response = new ApiResponse<bool>(result)
            {
                Message = new Message
                {
                    Description = "Success update user"
                }
            };

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var result = await _userService.DeleteUser(id);

            var response = new ApiResponse<bool>(result)
            {
                Message = new Message
                {
                    Description = "Success delete user"
                }
            };

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginRequest request)
        {
            var result = await _userService.LoginUser(request);

            var response = new ApiResponse<LoginResponse>(result)
            {
                Message = new Message
                {
                    Description = "Success login"
                }
            };

            return Ok(response);
        }

        [HttpGet("profile")]
        public async Task<IActionResult> ProfileUser()
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string[] tokens = token.ToString().Split(' ');

            var user = await _userService.GetUserByToken(tokens[1]);
            var userDto = _mapper.Map<UserResponse>(user);

            var response = new ApiResponse<UserResponse>(userDto);

            return Ok(response);
        }

        [HttpPut("update-password")]
        public async Task<IActionResult> UpdatePasswordUser(UpdatePasswordRequest request)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string[] tokens = token.ToString().Split(' ');

            var result = await _userService.UpdatePasswordUser(tokens[1], request);

            var response = new ApiResponse<bool>(result)
            {
                Message = new Message
                {
                    Description = "Success Update Password"
                }
            };

            return Ok(response);
        }
    }
}
