using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Models
{
    public class User : ClaimsPrincipal, IModel<string>, IAuthenticable
    {
        public string Id { get; set; }

        [Required]
        [JsonProperty("name")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [JsonProperty("email")]
        public string Email { get; set; }
        
        [DataType(DataType.Password)]
        [JsonProperty("password")]
        public string Password { get; set; }

        public string ResourcePath { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        public User()
        {
            this.ResourcePath = "users";
        }
    }
}
