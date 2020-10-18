using CostAccounting.Data.EntityFramework.Configurations.Core;
using CostAccounting.Data.EntityFramework.Configurations.Membership;
using CostAccounting.Data.EntityFramework.Configurations.Security;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CostAccounting.Data.EntityFramework
{
    public class CostAccountingContext : IdentityDbContext
    {
        public CostAccountingContext(DbContextOptions<CostAccountingContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new ExpenseConfiguration());
            builder.ApplyConfiguration(new IncomeConfiguration());
            builder.ApplyConfiguration(new RefreshTokenConfiguration());
        }
    }
}