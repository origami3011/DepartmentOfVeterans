using AutoMapper;
using DepartmentOfVeterans.Application.Features.Users.Commands.CreateUser;
using DepartmentOfVeterans.Application.Features.Users.Commands.DeleteUser;
using DepartmentOfVeterans.Application.Features.Users.Commands.UpdateUser;
using DepartmentOfVeterans.Application.Features.Users.Queries.GetUserDetail;
using DepartmentOfVeterans.Application.Features.Users.Queries.GetUserList;
using DepartmentOfVeterans.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentOfVeterans.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UsersListVM>().ReverseMap();    // two way mapping
            CreateMap<User, UserDetailVM>().ReverseMap();   // two way mapping
            CreateMap<User, CreateUserCommand>().ReverseMap();
            CreateMap<User, UpdateUserCommand>().ReverseMap();
            CreateMap<User, DeleteUserCommand>().ReverseMap();
        }
    }
}
