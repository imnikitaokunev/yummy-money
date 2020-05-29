using CostAccounting.Core.Entities;
using CostAccounting.Data.Configurations;
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

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new ExpenseConfiguration());
        }
    }
}