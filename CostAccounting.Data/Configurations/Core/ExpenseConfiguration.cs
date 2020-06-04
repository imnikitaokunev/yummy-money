using Microsoft.EntityFrameworkCore;
using CostAccounting.Core.Entities.Core;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CostAccounting.Data.Configurations.Core
{
    public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.ToTable(nameof(Expense));

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Amount).IsRequired();
            builder.Property(x => x.Date).IsRequired().HasColumnType("datetime2");
            builder.Property(x => x.Description).HasMaxLength(Expense.DescriptionLength);

            builder.HasOne(x => x.Category).WithMany().HasForeignKey(x => x.CategoryId);
        }
    }
}
