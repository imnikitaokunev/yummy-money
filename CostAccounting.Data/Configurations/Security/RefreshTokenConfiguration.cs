using CostAccounting.Core.Entities.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CostAccounting.Data.EntityFramework.Configurations.Security
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable(nameof(RefreshToken));

            builder.HasKey(x => x.Id);
            builder.Property(x => x.JwtId).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired().HasColumnType("datetime2");
            builder.Property(x => x.ExpiryDate).IsRequired().HasColumnType("datetime2");
            builder.Property(x => x.IsUsed).IsRequired();
            builder.Property(x => x.IsInvalidated).IsRequired();

            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
        }
    }
}