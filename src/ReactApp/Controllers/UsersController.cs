using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Services;

namespace ReactApp.Controllers
{
    public class UsersController : Controller
    {
        public async Task<IActionResult> Index()
        {
            Consumer consumer = Consumer.Create("https://api.smartsplit.eu/", 2, "jJZVb8GJVzroZgVpLtSwlHrwVUPEu1fqe3xCpHPk").Result;

            var users = await consumer.MakeCall("users");


            return View(users);
        }
    }
}
