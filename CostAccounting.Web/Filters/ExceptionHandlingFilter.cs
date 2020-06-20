using System.Net;
using System.Threading.Tasks;
using CostAccounting.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace CostAccounting.Web.Filters
{
    public class ExceptionHandlingFilter : IAsyncExceptionFilter
    {
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected
            var exception = context.Exception;
            var message = exception.Message;

            if (exception is RepositoryException)
            {
                code = HttpStatusCode.BadRequest;
                message = exception.InnerException?.InnerException?.Message ?? message;
            }

            // TODO: Logging can be added here.

            context.ExceptionHandled = true;

            var response = context.HttpContext.Response;
            response.StatusCode = (int)code;
            response.ContentType = "application/json";
            await response.WriteAsync(JsonConvert.SerializeObject(new { error = message }));
        }
    }
}
