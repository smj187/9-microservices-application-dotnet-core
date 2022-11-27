using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BuildingBlocks.Extensions.Http
{
    public static class HttpContextExtensions
    {
        public static async Task ResponseWithProblem(this HttpContext context, int statusCode, string title, string details)
        {
            context.Response.StatusCode = statusCode;
            var json = JsonSerializer.Serialize(new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Detail = details,
                Instance = context.Request.Path,
            });

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(json);
        }
    }
}
