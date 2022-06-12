using BuildingBlocks.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BuildingBlocks.Middleware
{
    public class Problem
    {

        public string? detail { get; set; } = null!;
        public string? instance { get; set; } = null!;
        public int status { get; set; }
        public string? title { get; set; } = null!;
        public string? type { get; set; } = null!;
        public string? traceId { get; set; } = null!;

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }

    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (AuthenticationException ex)
            {
                _logger.LogError(ex, $"Aggreate not found");
                await HandleAuthenticationException(httpContext, ex);
            }
            catch (AggregateNotFoundException ex)
            {
                _logger.LogError(ex, $"Aggreate not found");
                await HandleNotFunctionExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong");
                await HandleBadRequestExceptionAsync(httpContext, ex);
            }
        }

        private static async Task HandleAuthenticationException(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;

            await context.Response.WriteAsync(new Problem
            {
                type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                title = "Unauthorized",
                status = context.Response.StatusCode,
                detail = exception.Message,
                instance = context.Request.Path,
                traceId = "TODO: implement"
            }.ToString());
        }

        private static async Task HandleNotFunctionExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status404NotFound;

            await context.Response.WriteAsync(new Problem
            {
                type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                title = "Not Found",
                status = context.Response.StatusCode,
                detail = exception.Message,
                instance = context.Request.Path,
                traceId = "TODO: implement"
            }.ToString());
        }

        private static async Task HandleBadRequestExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            await context.Response.WriteAsync(new Problem
            {
                type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                title = "Bad Request",
                status = context.Response.StatusCode,
                detail = exception.Message,
                instance = context.Request.Path,
                traceId = "TODO: implement"
            }.ToString());
        }
    }
}
