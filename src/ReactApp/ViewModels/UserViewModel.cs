using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactApp.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string showUrl {
            get {
                return "";
            }
        }
    }
}
