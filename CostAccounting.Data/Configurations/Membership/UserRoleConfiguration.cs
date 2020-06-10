using CostAccounting.Core.Entities.Membership;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CostAccounting.Data.Configurations.Membership
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable(nameof(UserRole));

            builder.HasKey(x => new {x.UserId, x.RoleId});

            builder.HasOne(x => x.Role).WithMany().HasForeignKey(x => x.RoleId);
        }
    }
}
