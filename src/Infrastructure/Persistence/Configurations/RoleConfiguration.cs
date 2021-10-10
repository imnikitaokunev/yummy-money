//using Domain.Entities;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace Infrastructure.Persistence.Configurations
//{
//    public class RoleConfiguration : IEntityTypeConfiguration<Role>
//    {
//        public void Configure(EntityTypeBuilder<Role> builder)
//        {
//            builder.HasKey(x => x.Id);
//            builder.Property(x => x.Id).UseIdentityColumn();
//            builder.Property(x => x.Name).IsRequired().HasMaxLength(32);
//        }
//    }
//}
