using Banks.Model.Accounts;
using Banks.Model.Entities;
using Banks.Model.Transactions;
using Microsoft.EntityFrameworkCore;
using SQLite.CodeFirst;

namespace Banks.Repository.EF
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<CentralBank> CentralBanks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CreditAccount>();
            modelBuilder.Entity<DebitAccount>();
            modelBuilder.Entity<DepositAccount>();
            modelBuilder.Entity<MoneyTransfer>();
            modelBuilder.Entity<AccountReplenishment>();
            modelBuilder.Entity<FundsWithdrawal>();
            base.OnModelCreating(modelBuilder);
        }
    }
}