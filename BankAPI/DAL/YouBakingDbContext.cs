using BankAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
namespace BankAPI.DAL
{
    public class YouBakingDbContext : DbContext
    {
        public YouBakingDbContext(DbContextOptions<YouBakingDbContext> options) : base(options)
        {
        }
        public DbSet<Models.Account> Accounts { get; set; }
        public DbSet<Models.Transaction> Transactions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>()
                .Property(a => a.CurrentAccountBalance)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Transaction>()
                .Property(t => t.TransactionAmount)
                .HasPrecision(18, 2);
        }
    }
}
