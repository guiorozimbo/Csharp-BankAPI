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
    }
}
