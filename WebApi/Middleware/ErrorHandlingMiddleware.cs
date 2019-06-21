using System;
using System.Net;
using System.Threading.Tasks;
using DAL.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Serilog;
using WebApi.VIewDto;

namespace WebApi.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            var code = HttpStatusCode.InternalServerError;
            var errorMessage = e.Message;

            switch (e)
            {
                case EntryNotFoundException _:
                    code = HttpStatusCode.NotFound;
                    break;
                case ArgumentException _:
                    code = HttpStatusCode.BadRequest;
                    break;
                case DbUpdateException _:
                    code = HttpStatusCode.BadRequest;
                    errorMessage = "Entry with such name already exist. Name field should be unique.";
                    break;
            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            Log.Error(e, "{}", e.Message);
            var result = new ErrorDetails()
            {
                StatusCode = (int)code,
                Message = errorMessage
            }.ToString();
            return context.Response.WriteAsync(result);
        }
    }
}
