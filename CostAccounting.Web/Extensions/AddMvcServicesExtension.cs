using CostAccounting.Services.Services;
using CostAccounting.Services.Services.Implementation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CostAccounting.Web.Extensions
{
    public static class AddMvcServicesExtension
    {
        public static void AddMvcServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IExpenseService, ExpenseService>();

            // TODO: Add other services here.
        }
    }
}
