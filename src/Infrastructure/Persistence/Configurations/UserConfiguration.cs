using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Email).IsRequired().HasMaxLength(128);
            builder.Property(x => x.Username).IsRequired().HasMaxLength(128);
            builder.Property(x => x.PasswordHash).IsRequired().HasMaxLength(128);
            builder.Property(x => x.PasswordSalt).IsRequired().HasMaxLength(128);
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(128);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(128);
            builder.Property(x => x.RegisteredAt).IsRequired().HasColumnType("datetime2");

            builder.HasMany(x => x.Roles).WithMany(x => x.Users);
        }
    }
}
