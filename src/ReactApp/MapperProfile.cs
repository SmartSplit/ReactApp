using AutoMapper;
using Models;
using ReactApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactApp
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserViewModel>();

            CreateMap<UserViewModel, User>();

            CreateMap<User, UserListViewModel>();
        }
    }
}
