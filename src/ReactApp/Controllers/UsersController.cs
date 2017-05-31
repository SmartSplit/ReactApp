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
    [ApiAuthorized]
    public class UsersController : Controller
    {
        IRepositoryService<User> _usersService;
        private readonly IMapper _mapper;

        public UsersController(IRepositoryService<User> usersService, IMapper mapper)
        {
            _usersService = usersService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.User = _usersService.GetLoggedUser();

            try
            {
                var users = await _usersService.GetAll();

                UserListViewModel viewModel = new UserListViewModel()
                {
                    Users = users.Select(r => _mapper.Map<User, UserViewModel>(r)).ToList()
                };

                return View(viewModel);
            }
            catch (ApiCallException e)
            {
                ViewBag.Error = true;

                return View();
            }
        }

        public async Task<ActionResult> Details(string id)
        {
            ViewBag.User = _usersService.GetLoggedUser();

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

        //public IActionResult Create()
        //{
        //    var viewModel = new UserViewModel();

        //    return View(viewModel);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Create(UserViewModel viewModel)
        //{
        //    if (true)
        //    {
        //        User userToCreate = new User();

        //        _mapper.Map<UserViewModel, User>(viewModel, userToCreate);
        //        var result = await _usersService.Create(userToCreate);

        //        if (result.Result == ServiceResultStatus.Warning)
        //        {
        //            ViewBag.Errors = result.Messages;
        //        }

        //        if (result.Result == ServiceResultStatus.Success)
        //        {
        //            return RedirectToAction("Index");
        //        }
        //    }

        //    return View(viewModel);
        //}

        //public async Task<IActionResult> Edit(string id)
        //{
        //    if (id == "")
        //    {
        //        return new BadRequestResult();
        //    }

        //    var user = await _usersService.GetById(id);
        //    var viewModel = _mapper.Map<User, UserViewModel>(user);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }



        //    return View(viewModel);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Edit(UserViewModel viewModel)
        //{
        //    if(true)
        //    {
        //        User userToEdit = await _usersService.GetById(viewModel.Id);

        //        if(userToEdit != null)
        //        {
        //            _mapper.Map<UserViewModel, User>(viewModel, userToEdit);
        //            var result = await _usersService.Edit(userToEdit);

        //            if(result.Result == ServiceResultStatus.Warning)
        //            {
        //                ViewBag.Errors = result.Messages;
        //            }

        //            if (result.Result == ServiceResultStatus.Success)
        //            {
        //                return RedirectToAction("Index");
        //            }
        //        }
        //    }

        //    return View(viewModel);
        //}

        public async Task<IActionResult> LoadMorrisUsers()
        {
            _usersService.GetBuilder()
                .Filter("start_date", ">", DateTime.Today.AddDays(-30).ToString())
                .Limit(1000);

            MorrisDataBuilder morris = new MorrisDataBuilder();
            var users = (await _usersService.GetAll()).ToList();

            return Json(morris.dataForUsers(users));
        }


    }
}
