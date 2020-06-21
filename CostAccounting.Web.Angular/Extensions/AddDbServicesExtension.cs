using CostAccounting.Core.Repositories.Core;
using CostAccounting.Core.Repositories.Membership;
using CostAccounting.Core.Repositories.Security;
using CostAccounting.Data.EntityFramework;
using CostAccounting.Data.EntityFramework.Repositories.Core;
using CostAccounting.Data.EntityFramework.Repositories.Membership;
using CostAccounting.Data.EntityFramework.Repositories.Security;
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
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IExpenseRepository, ExpenseRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            // TODO: Add other repositories here.
            // TODO: Unit of work pattern can be used instead.
        }
    }
}