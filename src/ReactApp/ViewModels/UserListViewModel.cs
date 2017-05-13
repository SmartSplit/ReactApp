using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactApp.DTO
{
    public class UserViewModel
    {
        public List<UserDTO> Users { get; set; }

        public UserViewModel(List<UserDTO> users)
        {
            this.Users = users;
        }
    }
}
