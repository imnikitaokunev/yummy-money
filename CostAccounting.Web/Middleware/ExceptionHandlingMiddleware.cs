using System;
using System.Net;
using System.Threading.Tasks;
using CostAccounting.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CostAccounting.Web.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context /* other dependencies */)
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

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            if (ex is RepositoryException)
            {
                code = HttpStatusCode.BadRequest;
            }
            
            //if (ex is MyNotFoundException)
            //{
            //    code = HttpStatusCode.NotFound;
            //}
            //else if (ex is MyUnauthorizedException)
            //{
            //    code = HttpStatusCode.Unauthorized;
            //}
            //else if (ex is MyException)
            //{
            //    code = HttpStatusCode.BadRequest;
            //}

            var result = JsonConvert.SerializeObject(new {error = ex.Message});
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) code;
            return context.Response.WriteAsync(result);
        }
    }
}