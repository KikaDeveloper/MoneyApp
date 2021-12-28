using System;
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

        public async Task<IEnumerable<Wallet>> GetWallets()
        {
            var result = await _db.Wallets.ToListAsync();
            return result;
        }

        public async Task<bool> InsertWallet(Wallet wallet){
                var tracking = await _db.Wallets.AddAsync(wallet);
                await _db.SaveChangesAsync();
                return tracking.State == EntityState.Added;
        }

    }
}