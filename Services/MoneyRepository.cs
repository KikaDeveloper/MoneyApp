using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MoneyApp.Models;

namespace MoneyApp.Services
{
    public class MoneyRepository
    {
        private readonly AppContext _db;

        public MoneyRepository(string path){
            _db = new AppContext(path);
            _db.Database.EnsureCreated();
        }

        public async Task<IEnumerable<Wallet>> GetWalletsAsync()
            => await _db.Wallets.ToListAsync();

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
            => await _db.Categories.ToListAsync();

        public async Task<IEnumerable<Record>> GetRecordsAsync()
            => await _db.Records.ToListAsync();

        public async Task<IEnumerable<AmountRatio>> GetAmountRatiosAsync() 
            => await _db.AmountRatios.ToListAsync();

        public async Task<bool> InsertWalletAsync(Wallet wallet){
                var tracking = await _db.Wallets.AddAsync(wallet);
                await _db.SaveChangesAsync();
                return tracking.State == EntityState.Added;
        }

    }
}