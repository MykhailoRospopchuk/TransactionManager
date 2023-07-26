using Microsoft.EntityFrameworkCore;
using TransactionManagement.Model;

namespace TransactionManagement.Database
{
    public class TransactionDbContext : DbContext
    {
        public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options)
        {
        }

        public DbSet<TransactRecord> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TransactRecord>(entity =>
            {
                entity.HasKey(x => x.TransactionId);

                entity.Property(x => x.TransactionId)
                    .ValueGeneratedNever();

                entity.Property(x => x.Status)
                    .HasConversion<string>();
                entity.Property(x => x.Type)
                    .HasConversion<string>();
            });
        }
    }
}
