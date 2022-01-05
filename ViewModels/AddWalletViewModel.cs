using ReactiveUI;
using MoneyApp.Models;

namespace MoneyApp.ViewModels
{
    public class AddWalletViewModel : ViewModelBase
    {

        private string? _name;
        private int _amount;

        public string InputName
        {
            get => _name!;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        public int InputAmount
        {
            get => _amount;
            set => this.RaiseAndSetIfChanged(ref _amount, value);
        }

        public IReactiveCommand AddCommand { get; }

        public AddWalletViewModel(){
            
            AddCommand = ReactiveCommand.Create<Wallet>(() => {
                return new Wallet(){
                    Name = InputName,
                    Amount = InputAmount,
                    AmountRatioId = 1
                };
            });

        }
    }
}