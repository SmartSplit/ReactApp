using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Services;
using Newtonsoft.Json;
using Models;
using Newtonsoft.Json.Linq;
using ReactApp.ViewModels;
using AutoMapper;
using ReactApp.ViewModels.Dashboard;
using ReactApp.Filters;

namespace ReactApp.Controllers
{
    public class AccountController : Controller
    {
        IRepositoryService<User> _usersService;
        private readonly IMapper _mapper;

        public AccountController(IRepositoryService<User> usersService, IMapper mapper)
        {
            _usersService = usersService;
            _mapper = mapper;
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}
