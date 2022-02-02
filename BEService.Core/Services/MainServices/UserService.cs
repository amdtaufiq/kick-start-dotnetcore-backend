using BEService.Core.CustomEntities;
using BEService.Core.CustomEntities.Options;
using BEService.Core.DTOs;
using BEService.Core.Entities;
using BEService.Core.Exceptions;
using BEService.Core.Filters;
using BEService.Core.Interfaces.Services.MainServices;
using BEService.Core.Interfaces.Services.SupportServices;
using BEService.Core.Interfaces.Unit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace BEService.Core.Services.MainServices
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unit;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger _logger;
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService;
        private readonly PaginationOptions _paginationOptions;

        public UserService(
            IUnitOfWork unit,
            ILoggerFactory loggerFactory,
            IPasswordService passwordService,
            ITokenService tokenService,
            IOptions<PaginationOptions> paginationOptions)
        {
            _unit = unit;
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger("User");
            _passwordService = passwordService;
            _tokenService = tokenService;
            _paginationOptions = paginationOptions.Value;
        }

        public async Task<bool> AddUser(User user)
        {
            var cekEmail = _unit.UserRepository.GetUserByEmail(user.Email);
            if(cekEmail != null)
            {
                throw new UnprocessableEntityException("email already exists");
            }
            try
            {
                user.Password = _passwordService.Hash(user.Password);

                await _unit.UserRepository.Add(user);
                await _unit.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("User Add => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        public async Task<bool> DeleteUser(Guid id)
        {
            try
            {
                var data = await _unit.UserRepository.GetById(id);
                if (data == null)
                {
                    throw new NotFoundException("User doesn't exist!");
                }
                _unit.UserRepository.Delete(data);
                await _unit.SaveChangesAsync(true);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("User Delete => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        public PagedList<User> GetAllUser(UserFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;

            try
            {
                var datas = _unit.UserRepository.GetAll();

                return PagedList<User>.Create(datas, filters.PageNumber, filters.PageSize);
            }
            catch (Exception e)
            {
                _logger.LogError("User List => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        public async Task<User> GetUserById(Guid id)
        {
            try
            {
                var data = await _unit.UserRepository.GetById(id);
                if (data == null)
                {
                    throw new NotFoundException("User doesn't exist!");
                }
                return data;
            }
            catch (Exception e)
            {
                _logger.LogError("User By ID => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        public async Task<bool> UpdateUser(Guid id, User user)
        {
            try
            {
                var data = await _unit.UserRepository.GetById(id);
                if (data == null)
                {
                    throw new NotFoundException("User doesn't exist!");
                }

                //Set value
                data.Name = user.Name;
                data.Email = user.Email;
                data.RoleId = user.RoleId;

                _unit.UserRepository.Update(data);
                await _unit.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("User Update => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        public async Task<User> GetUserByToken(string token)
        {
            try
            {
                var data = await _unit.UserRepository.GetUserByToken(token);
                if (data == null)
                {
                    throw new NotFoundException("User doesn't exist!");
                }
                return data;
            }
            catch (Exception e)
            {
                _logger.LogError("User By Token => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        public async Task<LoginResponse> LoginUser(LoginRequest request)
        {
            var user = await _unit.UserRepository.GetUserByEmail(request.Email);
            if(user == null)
            {
                throw new NotFoundException("User doesn't exist!");
            }
            var isValidPassword = _passwordService.Check(user.Password, request.Password);
            if(isValidPassword == false)
            {
                throw new UnprocessableEntityException("Your password is wrong");
            }

            try
            {
                var result = _tokenService.GenerateTokenUser(user);

                //update token
                user.Token = result.Token;
                _unit.UserRepository.Update(user);
                await _unit.SaveChangesAsync();

                return result;
            }
            catch (Exception e)
            {
                _logger.LogError("User Login => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        public async Task<bool> UpdatePasswordUser(string token, UpdatePasswordRequest request)
        {
            var user = await _unit.UserRepository.GetUserByToken(token);
            if(token == null)
            {
                throw new NotFoundException("User doesn't exist!");
            }

            var isValidPassword = _passwordService.Check(user.Password, request.CurrentPassword);
            if (isValidPassword == false)
            {
                throw new UnprocessableEntityException("Your password is wrong");
            }

            try
            {
                //update password
                user.Password = _passwordService.Hash(request.Password);
                _unit.UserRepository.Update(user);
                await _unit.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("User Update Password => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }
    }
}
