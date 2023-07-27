using Microsoft.EntityFrameworkCore;
using TransactionManagement.Database.Configuration;
using TransactionManagement.Model.Entities;

namespace TransactionManagement.Database
{
    public class TransactionDbContext : DbContext
    {
        public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserRefreshTokenConfiguration());

            modelBuilder.ApplyConfiguration(new TransactRecordConfiguration());
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }

        public DbSet<TransactRecord> Transactions { get; set; }
    }
}
