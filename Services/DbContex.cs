using Microsoft.EntityFrameworkCore;
using MoneyApp.Models;

namespace MoneyApp.Services{

    public class AppContext : DbContext
    {
        private readonly string _dbPath;

        public DbSet<Wallet> Wallets { get; set; } = default!;
        public DbSet<Category> Categories { get; set; } = default!;
        public DbSet<Record> Records { get; set; } = default!;

        public AppContext(string dbPath){
            _dbPath = dbPath;
        } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite($"DataSource={_dbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Wallet>().HasData(
                new Wallet[]{
                    new Wallet(){
                        Id = 1,
                        Name = "wallet",
                        Amount = 15000, 
                        AmountRatio = AmountRatio.Day
                    }
                }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category[]{
                    new Category(){
                        Id = 1,
                        Name = "Shop",
                        Amount = 1500,
                        IconPath = "sklfk",
                        WalletId = 1
                    }
                }
            );
        }

    }

}