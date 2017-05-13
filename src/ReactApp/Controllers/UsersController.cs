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
using ReactApp.DTO;

namespace ReactApp.Controllers
{
    public class UsersController : Controller
    {
        IRepositoryService<User> _usersService;

        public UsersController(IRepositoryService<User> usersService)
        {
            _usersService = usersService;
        }

        public async Task<IActionResult> Index()
        {
            //Consumer consumer = Consumer.Create().Result;

            //var usersResponseString = await consumer.MakeCall("users");

            //var usersResponseObject = JsonConvert.DeserializeObject<ApiResponse>(usersResponseString);

            ApiResponse usersResponseObject = await _usersService.GetAll();

            var users = JsonConvert.DeserializeObject<List<UserDTO>>((usersResponseObject.data.ToString()));

            var userListDTO = new UserListDTO(users);

            return View(userListDTO);
        }
    }
}
