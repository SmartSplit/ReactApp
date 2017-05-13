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

namespace ReactApp.Controllers
{
    public class UsersController : Controller
    {
        IRepositoryService<User> _usersService;
        private readonly IMapper _mapper;

        public UsersController(IRepositoryService<User> usersService, IMapper mapper)
        {
            _usersService = usersService;
            _mapper = mapper;
        }

        [Route("users", Name = "Users")]
        public async Task<IActionResult> Index()
        {
            var users = await _usersService.GetAll();

            UserListViewModel viewModel = new UserListViewModel()
            {
                Users = users.Select(r => _mapper.Map<User, UserViewModel>(r)).ToList()
            };

            return View(viewModel);
        }

        [Route("users/{id}", Name = "User")]
        public async Task<ActionResult> Details(string id)
        {
            if(id == "")
            {
                return new BadRequestResult();
            }

            var user = await _usersService.GetById(id);
            var viewModel = _mapper.Map<User, UserViewModel>(user);

            if (user == null)
            {
                return NotFound();
            }


            return View(viewModel);
        }

        [Route("users/{id}/edit", Name = "UserEditForm")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == "")
            {
                return new BadRequestResult();
            }

            var user = await _usersService.GetById(id);
            var viewModel = _mapper.Map<User, UserViewModel>(user);

            if (user == null)
            {
                return NotFound();
            }



            return View(viewModel);
        }
    }
}
