using Banks.Model.Accounts;
using Banks.Model.Entities;
using Banks.Model.Transactions;
using Microsoft.EntityFrameworkCore;

namespace Banks.Repository.EF
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<CentralBank> CentralBanks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;");
            optionsBuilder.UseInMemoryDatabase("banksdb");
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