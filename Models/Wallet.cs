
namespace MoneyApp.Models{
    public class Wallet
    {   
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int Amount { get; set; }
        public int AmountRatioId { get; set; }
    }
}