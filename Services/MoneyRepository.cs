using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MoneyApp.Models;

namespace MoneyApp.Services
{
    public class MoneyRepository
    {
        private readonly AppContext _db;
        private static string _path = "money.db";
        private static MoneyRepository? _instance;
        public static MoneyRepository Instance 
        { 
            get {
                if(_instance == null)
                    _instance = new MoneyRepository();
                return _instance;
            }
        }

        protected MoneyRepository(){
            _db = new AppContext(_path);
            _db.Database.EnsureCreated();
        }

        #region GetCollections
        public async Task<IEnumerable<Wallet>> GetWalletsAsync()
            => await _db.Wallets.ToListAsync();

        public async Task<IEnumerable<Category>> GetCategoriesAsync(int walletId)
            => await _db.Categories.Where(c => c.WalletId == walletId).ToListAsync();

        public async Task<IEnumerable<Record>> GetRecordsAsync(int categoryId)
            => await _db.Records.Where(r => r.CategoryId == categoryId).ToListAsync();

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