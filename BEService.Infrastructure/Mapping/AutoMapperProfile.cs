using AutoMapper;
using BEService.Core.DTOs;
using BEService.Core.Entities;

namespace BEService.Infrastructure.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Mapping Role
            CreateMap<Role, RoleResponse>();

            CreateMap<CreateRoleRequest, Role>();

            CreateMap<UpdateRoleRequest, Role>();

            //Mapping User
            CreateMap<User, UserResponse>();

            CreateMap<CreateUserRequest, User>();

            CreateMap<UpdateUserRequest, User>();

            //Mapping MenuApp
            CreateMap<MenuApp, MenuAppResponse>();

            CreateMap<CreateMenuAppRequest, MenuApp>();

            CreateMap<UpdateMenuAppRequest, MenuApp>();

            //Mapping MenuAccess
            CreateMap<MenuAccess, MenuAccessResponse>();

            CreateMap<CreateMenuAccessRequest, MenuAccess>();

            CreateMap<UpdateMenuAccessRequest, MenuAccess>();
        }
    }
}
