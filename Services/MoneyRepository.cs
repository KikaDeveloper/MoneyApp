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

        #region GetCollections
        public async Task<IEnumerable<Wallet>> GetWalletsAsync()
            => await _db.Wallets.ToListAsync();

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
            => await _db.Categories.ToListAsync();

        public async Task<IEnumerable<Record>> GetRecordsAsync()
            => await _db.Records.ToListAsync();

        #endregion
        
        public async Task<bool> InsertWalletAsync(Wallet wallet){
                var tracking = await _db.Wallets.AddAsync(wallet);
                await _db.SaveChangesAsync();
                return tracking.State == EntityState.Added;
        }
        
        public async Task<bool> DeleteWalletAsync(Wallet wallet){
            var tracking = _db.Wallets.Remove(wallet);
            await _db.SaveChangesAsync();
            return tracking.State == EntityState.Deleted;
        }

        public async Task<bool> UpdateWalletAsync(Wallet wallet){
            var tracking = _db.Wallets.Update(wallet);
            await _db.SaveChangesAsync();
            return tracking.State == EntityState.Modified;
        }
    
        public async Task<bool> InsertCategoryAsync(Category category){
            var tracking = await _db.Categories.AddAsync(category);
            await _db.SaveChangesAsync();
            return tracking.State == EntityState.Added;
        }

        public async Task<bool> DeleteCategoryAsync(Category category){
            var tracking = _db.Categories.Remove(category);
            await _db.SaveChangesAsync();
            return tracking.State == EntityState.Deleted;
        }

        public async Task<bool> UpdateCategoryAsync(Category category){
            var tracking = _db.Categories.Update(category);
            await _db.SaveChangesAsync();
            return tracking.State == EntityState.Modified;
        }

        public async Task<bool> InsertRecordAsync(Record record){
            var tracking = await _db.Records.AddAsync(record);
            await _db.SaveChangesAsync();
            return tracking.State == EntityState.Added;
        }

        public async Task<bool> DeleteRecordAsync(Record record){
            var tracking = _db.Records.Remove(record);
            await _db.SaveChangesAsync();
            return tracking.State == EntityState.Deleted;
        }
        
        public async Task<bool> UpdateRecordAsync(Record record){
            var tracking = _db.Records.Remove(record);
            await _db.SaveChangesAsync();
            return tracking.State == EntityState.Modified;
        }
    }
}