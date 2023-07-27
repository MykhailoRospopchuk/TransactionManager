using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransactionManagement.Model.Entities;
using TransactionManagement.Model.Enums;

namespace TransactionManagement.Database.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder
            .HasKey(x => x.Id);

            builder.HasData
            (
                new Role { RoleName = RoleEnum.Admin, Id = 1 },
                new Role { RoleName = RoleEnum.User, Id = 2 }
            );
        }
    }
}
