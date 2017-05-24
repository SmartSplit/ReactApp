using AutoMapper;
using Models;
using ReactApp.ViewModels;
using ReactApp.ViewModels.Items;
using ReactApp.ViewModels.Payments;
using ReactApp.ViewModels.Sessions;
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

            CreateMap<Session, SessionViewModel>();

            CreateMap<Item, ItemViewModel>()
                .ForMember(viewModel => viewModel.Price, opt => opt.ResolveUsing<PriceResolver>());

            CreateMap<Payment, PaymentViewModel>();
        }
    }
}
