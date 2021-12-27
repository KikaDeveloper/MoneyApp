
namespace MoneyApp.Models{

    public class AmountRatio
    {
        public int Id { get; set; }
        public string Name { get; set;} = default!;
        public int Ratio {get; set; }

        public override string ToString() => Name;
    }

}