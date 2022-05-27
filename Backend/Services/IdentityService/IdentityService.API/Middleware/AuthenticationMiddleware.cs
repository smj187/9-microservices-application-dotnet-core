using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.API.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {


            var auth = context.Request.Headers["Authorization"];
            var token = auth.FirstOrDefault()?[7..];


            if (token != null)
            {
                Console.WriteLine($"{context.Request.Path.ToString()} -> yes");
                //context.Response.StatusCode = StatusCodes.Status200OK;
                //await context.Response.WriteAsync("success");
                // await _next(context);
                // return instantly
                //return;
            }
            else
            {
                Console.WriteLine($"{context.Request.Path.ToString()} -> no :(");
                // context.Response.Clear();
                // context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                // await context.Response.WriteAsync("Unauthorized - Token Not Found");
            }

            await _next(context);
        }
    }
}
