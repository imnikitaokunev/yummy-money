using CostAccounting.Data.Configurations.Core;
using CostAccounting.Data.Configurations.Membership;
using CostAccounting.Data.Configurations.Security;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CostAccounting.Data
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
            builder.ApplyConfiguration(new RefreshTokenConfiguration());
        }
    }
}