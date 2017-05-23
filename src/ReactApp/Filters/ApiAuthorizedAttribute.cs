using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
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
                var authorization = context.HttpContext.Request.Headers.["Authorization"];
                Console.WriteLine("API AUTHORIZED FILTER");
                await next();
            }
        }
    }
}
