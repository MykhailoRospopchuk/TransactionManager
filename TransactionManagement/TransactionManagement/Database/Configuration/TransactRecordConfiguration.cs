using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using TransactionManagement.Model.Entities;

namespace TransactionManagement.Database.Configuration
{
    public class TransactRecordConfiguration : IEntityTypeConfiguration<TransactRecord>
    {
        public void Configure(EntityTypeBuilder<TransactRecord> builder)
        {

            builder.HasKey(x => x.TransactionId);

            builder.Property(x => x.TransactionId)
                 .ValueGeneratedNever();

            builder.Property(x => x.Status)
                .HasConversion<string>();

            builder.Property(x => x.Type)
                 .HasConversion<string>();

        }
    }
}
