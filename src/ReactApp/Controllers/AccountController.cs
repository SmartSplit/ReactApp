﻿using System;
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
using Microsoft.AspNetCore.Http;

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
            UserViewModel vm = new UserViewModel();

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserViewModel vm)
        {
            User user = new User();

            user = _mapper.Map<UserViewModel, User>(vm, user);

            var result = await _usersService.Login(user);

            if(result.Result == ServiceResultStatus.Warning)
            {
                ViewBag.Errors = result.Messages;
                return View(vm);
            }

            //HttpContext.Session.SetString("User", JsonConvert.SerializeObject(user));
            

            return RedirectToAction("Index", "Users");
        }

        public IActionResult Logout()
        {
            _usersService.Logout();

            return RedirectToAction("Login");
        }

        public IActionResult Register()
        {
            UserViewModel vm = new UserViewModel();
            

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = new User();

                _mapper.Map<UserViewModel, User>(vm, user);
                var result = await _usersService.Register(user);

                if (result.Result == ServiceResultStatus.Warning)
                {
                    ViewBag.Errors = result.Messages;
                }

                if (result.Result == ServiceResultStatus.Success)
                {
                    await _usersService.Login(user);

                    return RedirectToAction("Index", "Dashboard");
                }
            }

            return View(vm);
        }
    }
}
