using Models;
using ReactApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReactApp.Validators
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class PasswordConfirmationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            UserViewModel vm = context.ObjectInstance as UserViewModel;

            if (vm.Password != vm.PasswordConfirmation)
            {
                return new ValidationResult(ErrorMessageString);
            }

            return ValidationResult.Success;
        }
    }
}
