using CostAccounting.Core.Repositories.Core;
using CostAccounting.Data;
using CostAccounting.Data.Repositories.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CostAccounting.Web.Extensions
{
    public static class AddDbServicesExtension
    {
        public static void AddDbServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CostAccountingContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<CostAccountingContext>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IExpenseRepository, ExpenseRepository>();

            // TODO: Add other repositories here.
            // TODO: Unit of work pattern can be used instead.
        }
    }
}