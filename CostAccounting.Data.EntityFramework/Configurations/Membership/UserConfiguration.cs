using CostAccounting.Core.Entities.Membership;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CostAccounting.Data.EntityFramework.Configurations.Membership
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(User.EmailLength);
            builder.Property(x => x.Username).IsRequired().HasMaxLength(User.UsernameMaxLength);
            builder.Property(x => x.PasswordHash).IsRequired().HasMaxLength(User.PasswordHashLength);
            builder.Property(x => x.PasswordSalt).IsRequired().HasMaxLength(User.PasswordSaltLength);
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(User.FirstNameLength);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(User.LastNameLength);
            builder.Property(x => x.RegisteredAt).IsRequired().HasColumnType("datetime2");
            builder.Property(x => x.Photo);

            builder.HasMany(x => x.Roles).WithOne().HasForeignKey(x => x.UserId);
        }
    }
}
