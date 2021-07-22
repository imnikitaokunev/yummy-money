using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using Serilog.Context;

namespace CostAccounting.Web.Angular.Filters
{
    public class ExceptionHandlingFilter : IExceptionFilter
    {
        private readonly ILogger _logger;

        public ExceptionHandlingFilter(ILogger logger)
        {
            _logger = logger.ForContext<ExceptionHandlingFilter>();
        }

        public void OnException(ExceptionContext context)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected
            var exception = context.Exception;

            using (LogContext.PushProperty("UserId", context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id")))
            {
                _logger.Error(exception, "An error occurred");
            }

            context.ExceptionHandled = true;

            var response = context.HttpContext.Response;
            response.StatusCode = (int)code;
        }
    }
}
