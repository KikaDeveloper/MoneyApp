
namespace MoneyApp.Models{

    public class Category{
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int Amount { get; set; }
        public int WalletId { get; set; }
    }

}