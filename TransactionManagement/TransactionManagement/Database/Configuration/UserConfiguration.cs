using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransactionManagement.Model.Entities;

namespace TransactionManagement.Database.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
            .HasOne(x => x.UserRefreshToken)
            .WithOne(x => x.User)
            .HasForeignKey<UserRefreshToken>(x => x.UserId);

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Email)
                .HasMaxLength(50);

            builder
                .Property(x => x.Password)
                .HasMaxLength(50);

            builder
                .Property(x => x.FirstName)
                .HasMaxLength(50);

            builder
                .Property(x => x.LastName)
                .HasMaxLength(50);

            builder
                .HasData
                (
                    new User
                    {
                        Id = 1,
                        RoleId = 1,
                        Email = "bigbos@gmail.com",
                        Password = "bigbos",
                        FirstName = "Admin",
                        LastName = "Admin"
                    }
                );
        }
    }
}
