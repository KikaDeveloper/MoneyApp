
namespace MoneyApp.Models{

    public class Category{
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int Amount { get; set; }
        public string IconPath { get; set; } = default!;
        public int WalletId { get; set; }
    }

}