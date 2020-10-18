using CostAccounting.Core.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CostAccounting.Data.EntityFramework.Configurations.Core
{
    public class IncomeConfiguration : IEntityTypeConfiguration<Income>
    {
        public void Configure(EntityTypeBuilder<Income> builder)
        {
            builder.ToTable(nameof(Income));

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Amount).IsRequired();
            builder.Property(x => x.Date).IsRequired().HasColumnType("datetime2");
            builder.Property(x => x.Description).HasMaxLength(Expense.DescriptionLength);

            builder.HasOne(x => x.Category).WithMany().HasForeignKey(x => x.CategoryId);
            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
        }
    }
}