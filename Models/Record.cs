
namespace MoneyApp.Models{
    public class Record
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int Amount { get; set; }
        public int CategoryId { get; set; }
    }
}