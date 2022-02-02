using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MoneyApp.Models;

namespace MoneyApp.Services
{
    public class MoneyRepository
    {
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
            using(var context = new AppContext(_path))
            {
                context.Database.EnsureCreated();
            }
        }

        #region GetCollections
        public async Task<IEnumerable<Wallet>> GetWalletsAsync() 
        {
            using(var context = new AppContext(_path))
            {
                return await context.Wallets.ToListAsync();
            }
        }   
            

        public async Task<IEnumerable<Category>> GetCategoriesAsync(int walletId)
        {
              using(var context = new AppContext(_path))
            {
                return await context.Categories.Where(c => c.WalletId == walletId).ToListAsync();
            }
        }

        public async Task<IEnumerable<Record>> GetRecordsAsync(int categoryId)
        {
            using(var context = new AppContext(_path))
            {
                return await context.Records.Where(r => r.CategoryId == categoryId).ToListAsync();
            }
        }

        #endregion
        
        public async Task<bool> InsertWalletAsync(Wallet wallet){
            using(var context = new AppContext(_path))
            {
                var tracking = await context.Wallets.AddAsync(wallet);
                await context.SaveChangesAsync();
                return tracking.State == EntityState.Added;
            }
        }
        
        public async Task<bool> DeleteWalletAsync(Wallet wallet){
            using(var context = new AppContext(_path))
            {
                var tracking = context.Wallets.Remove(wallet);
                await context.SaveChangesAsync();
                return tracking.State == EntityState.Deleted;
            }
        }

        public async Task<bool> UpdateWalletAsync(Wallet wallet){
            using(var context = new AppContext(_path))
            {
                var tracking = context.Wallets.Update(wallet);
                await context.SaveChangesAsync();
                return tracking.State == EntityState.Modified;
            }
        }
    
        public async Task<bool> InsertCategoryAsync(Category category){
            using(var context = new AppContext(_path))
            {
                var tracking = await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();
                return tracking.State == EntityState.Added;
            }
        }

        public async Task<bool> DeleteCategoryAsync(Category category){
            using(var context = new AppContext(_path))
            {
                var tracking = context.Categories.Remove(category);
                await context.SaveChangesAsync();
                return tracking.State == EntityState.Deleted;
            }
        }

        public async Task<bool> UpdateCategoryAsync(Category category){
            using(var context = new AppContext(_path))
            {
                var tracking = context.Categories.Update(category);
                await context.SaveChangesAsync();
                return tracking.State == EntityState.Modified;
            }
        }

        public async Task<bool> InsertRecordAsync(Record record){
            using(var context = new AppContext(_path))
            {
                var tracking = await context.Records.AddAsync(record);
                await context.SaveChangesAsync();
                return tracking.State == EntityState.Added;
            }
        }

        public async Task<bool> DeleteRecordAsync(Record record){
            using(var context = new AppContext(_path))
            {
                var tracking = context.Records.Remove(record);
                await context.SaveChangesAsync();
                return tracking.State == EntityState.Deleted;
            }
        }
        
        public async Task<bool> UpdateRecordAsync(Record record){
            using(var context = new AppContext(_path))
            {
                var tracking = context.Records.Remove(record);
                await context.SaveChangesAsync();
                return tracking.State == EntityState.Modified;
            }
        }
    }
}