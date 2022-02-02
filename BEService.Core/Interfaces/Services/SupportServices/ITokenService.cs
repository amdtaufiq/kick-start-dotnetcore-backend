using BEService.Core.DTOs;
using BEService.Core.Entities;

namespace BEService.Core.Interfaces.Services.SupportServices
{
    public interface ITokenService
    {
        LoginResponse GenerateTokenUser(User user);
    }
}
