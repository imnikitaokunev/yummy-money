using System.Linq;
using System.Threading.Tasks;
using CostAccounting.Services.Models.Error;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CostAccounting.Web.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage)).ToArray();

                var response = new ValidationErrorResponse();

                foreach (var (key, value) in errors)
                {
                    foreach (var subError in value)
                    {
                        var errorModel = new ValidationErrorModel
                        {
                            FieldName = key,
                            Message = subError
                        };

                        response.Errors.Add(errorModel);
                    }
                }

                context.Result = new BadRequestObjectResult(response);
                return;
            }

            await next();
        }
    }
}