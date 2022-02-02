using BEService.Core.CustomEntities;
using BEService.Core.DTOs;
using BEService.Core.Entities;
using BEService.Core.Filters;
using System;
using System.Threading.Tasks;

namespace BEService.Core.Interfaces.Services.MainServices
{
    public interface IUserService
    {
        PagedList<User> GetAllUser(UserFilter filters);
        Task<User> GetUserById(Guid id);
        Task<bool> AddUser(User user);
        Task<bool> UpdateUser(Guid id, User user);
        Task<bool> DeleteUser(Guid id);
        Task<User> GetUserByToken(string token);
        Task<LoginResponse> LoginUser(LoginRequest request);
        Task<bool> UpdatePasswordUser(string token, UpdatePasswordRequest request);
    }
}
