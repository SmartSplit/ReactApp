using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactApp.ViewModels
{
    public class UserListViewModel
    {
        public List<UserViewModel> Users { get; set; }

        public UserListViewModel(List<UserViewModel> users)
        {
            this.Users = users;
        }

        public UserListViewModel()
        {

        }
    }
}
