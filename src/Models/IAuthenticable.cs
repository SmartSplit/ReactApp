using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public interface IAuthenticable
    {
        string Id { get; set; }
        string Email { get; set; }
        string Password { get; set; }
    }
}
