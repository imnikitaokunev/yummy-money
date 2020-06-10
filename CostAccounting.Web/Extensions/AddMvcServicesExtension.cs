using CostAccounting.Services.Implementation.Core;
using CostAccounting.Services.Implementation.Membership;
using CostAccounting.Services.Interfaces.Core;
using CostAccounting.Services.Interfaces.Membership;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CostAccounting.Web.Extensions
{
    public static class AddMvcServicesExtension
    {
        public static void AddMvcServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IExpenseService, ExpenseService>();

            // TODO: Add other services here.
        }
    }
}