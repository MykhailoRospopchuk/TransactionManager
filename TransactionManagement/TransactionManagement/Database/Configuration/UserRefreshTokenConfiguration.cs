using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransactionManagement.Model.Entities;

namespace TransactionManagement.Database.Configuration
{
    public class UserRefreshTokenConfiguration : IEntityTypeConfiguration<UserRefreshToken>
    {
        public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
        {
            builder
            .HasKey(x => x.Id);

            builder
                .Property(x => x.Token);

            builder
                .Property(x => x.Expires);
        }
    }
}
