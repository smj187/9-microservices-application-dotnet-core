using BuildingBlocks.Exceptions.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BuildingBlocks.Middleware.Exceptions
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (AggregateNotFoundException ex)
            {
                _logger.LogError(ex, ex.Message);


                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                var json = JsonSerializer.Serialize(new ProblemDetails
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Title = $"{ex.Message} not found",
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Detail = $"{ex.Message}\n{ex.Message}",
                    Instance = context.Request.Path,
                });

                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);


                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var json = JsonSerializer.Serialize(new ProblemDetails
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Title = "Internal Server Error",
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Detail = $"{ex.Message}\n{ex.Message}",
                    Instance = context.Request.Path,
                });

                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json);
            }
        }
    }
}
