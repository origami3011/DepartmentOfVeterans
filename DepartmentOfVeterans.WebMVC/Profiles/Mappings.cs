using AutoMapper;
using DepartmentOfVeterans.App.Services;
using DepartmentOfVeterans.WebMVC.Models;

namespace DepartmentOfVeterans.WebMVC.Profiles
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            //Vms are coming in from the API, ViewModel are the local entities in MVC
            CreateMap<UsersListVM, UserViewModel>().ReverseMap();
            CreateMap<UserDetailVM, UserViewModel>().ReverseMap();

            CreateMap<CreateUserCommand, UserViewModel>().ReverseMap();
            CreateMap<UpdateUserCommand, UserViewModel>().ReverseMap();
            CreateMap<CreateUserCommand, UserViewModel>().ReverseMap();
        }
    }
}
