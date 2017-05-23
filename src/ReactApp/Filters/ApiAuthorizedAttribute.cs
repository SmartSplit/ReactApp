using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ReactApp.Filters
{
    public class ApiAuthorizedAttribute : TypeFilterAttribute
    {
        public ApiAuthorizedAttribute():base(typeof (ApiAuthorizedFilterImpl))
        {
        }

        private class ApiAuthorizedFilterImpl : IAsyncActionFilter
        {
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                var authorization = context.HttpContext.Request.Headers["Authorization"];

                var user = context.HttpContext.User as User;

                if (user == null)
                {
                    //todo generate url
                    context.Result = new RedirectResult("Account/Login");
                    return;
                }

                await next();
            }
        }
    }
}
