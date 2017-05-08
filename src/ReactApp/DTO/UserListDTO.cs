using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactApp.DTO
{
    public class UserListDTO
    {
        public List<UserDTO> Users { get; set; }

        public UserListDTO(List<UserDTO> users)
        {
            this.Users = users;
        }
    }
}
