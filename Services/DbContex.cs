using Microsoft.EntityFrameworkCore;

namespace MoneyApp.Services{

    public class AppContext : DbContext
    {
        private readonly string _dbPath;

        public AppContext(string dbPath){
            _dbPath = dbPath;
        } 

        protected override void  OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            optionsBuilder.UseSqlite($"DataSource={_dbPath}");
        }

    }

}