using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using RedOne.Rewards.Application.Exceptions;
using RedOne.Rewards.WebApi.Dtos;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace RedOne.Rewards.WebApi.Middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = StatusCodes.Status500InternalServerError;

            var result = string.Empty;

            switch (exception)
            {
                case NotFoundException ex:
                    code = StatusCodes.Status404NotFound;
                    result = ex.Message;
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            if (result == string.Empty)
            {
                result = JsonSerializer.Serialize(new ErrorDto(exception.Message));
            }
            else
            {
                result = JsonSerializer.Serialize(new ErrorDto(result));
            }

            return context.Response.WriteAsync(result);
        }
    }

    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}
