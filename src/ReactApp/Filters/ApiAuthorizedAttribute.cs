using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Models;
using Newtonsoft.Json;
using Services;
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
            public IConsumer consumer { get; set; }

            public ApiAuthorizedFilterImpl(IConsumer _consumer)
            {
                consumer = _consumer;
            }
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                var authorization = context.HttpContext.Request.Headers["Authorization"];
                string jsonUser = context.HttpContext.Session.GetString("User");

                

                if (consumer.GetUser() == null)
                {
                    //todo generate url
                    context.Result = new RedirectResult("Account/Login");
                    return;
                }

                //User user = JsonConvert.DeserializeObject<User>(jsonUser);

                //context.ActionArguments["User"] = user;

                await next();
            }
        }
    }
}
