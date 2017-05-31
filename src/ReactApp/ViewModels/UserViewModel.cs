using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ReactApp.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReactApp.ViewModels
{
    public class UserViewModel :GenericViewModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [Required]
        [JsonProperty("name")]
        public string Name { get; set; }

        [Required]
        [JsonProperty("email")]
        public string Email { get; set; }

        [Required]
        [DataType("Password")]
        [JsonProperty("password")]
        public string Password { get; set; }

        [Required]
        [PasswordConfirmation(ErrorMessage = "Password confimation does not match.")]
        [JsonProperty("password_confirmation")]
        public string PasswordConfirmation { get; set; }

        public string showUrl {
            get {
                return "";
            }
        }
    }
}
