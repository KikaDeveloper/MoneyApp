using Microsoft.EntityFrameworkCore;
using MoneyApp.Models;

namespace MoneyApp.Services{

    public class AppContext : DbContext
    {
        private readonly string _dbPath;

        public DbSet<Wallet> Wallets { get; set; } = default!;
        public DbSet<Category> Categories { get; set; } = default!;
        public DbSet<Record> Records { get; set; } = default!;
        public DbSet<AmountRatio> AmountRatios { get; set; } = default!;

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
                        AmountRatioId = 1
                    },
                    new Wallet(){
                        Id = 2,
                        Name = "wallet2",
                        Amount = 1300,
                        AmountRatioId = 2
                    }
                }
            );

            modelBuilder.Entity<AmountRatio>().HasData(
                new AmountRatio[]{
                    new AmountRatio(){
                        Id = 1,
                        Name = "day",
                        Ratio = 1
                    },
                    new AmountRatio(){
                        Id = 2,
                        Name = "week",
                        Ratio = 7
                    },
                    new AmountRatio(){
                        Id = 3,
                        Name = "month",
                        Ratio = 31
                    },
                    new AmountRatio(){
                        Id = 4,
                        Name = "six months",
                        Ratio = 186,
                    },
                    new AmountRatio(){
                        Id = 5,
                        Name = "year",
                        Ratio = 366
                    }
                }
            );
        }

    }

}