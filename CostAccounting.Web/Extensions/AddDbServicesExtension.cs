using CostAccounting.Core.Repositories;
using CostAccounting.Data;
using CostAccounting.Data.Repositories;
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

            services.AddScoped<ICategoryRepository, CategoryRepository>();

            // TODO: Add other repositories here.
            // TODO: Unit of work pattern can be used instead.
        }
    }
}
