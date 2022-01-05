using System.Collections.ObjectModel;
using ReactiveUI;
using MoneyApp.Models;

namespace MoneyApp.ViewModels
{
    public class AddWalletViewModel : ViewModelBase
    {

        private string? _name;
        private int _amount;
        private AmountRatio? _selectedRatio;

        private ReadOnlyCollection<AmountRatio>? _amountRatios;

        public ReadOnlyCollection<AmountRatio> AmountRatios
        {
            get => _amountRatios!;
            set => this.RaiseAndSetIfChanged(ref _amountRatios, value);
        }

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

        public AmountRatio SelectedRatio
        {
            get => _selectedRatio!;
            set => this.RaiseAndSetIfChanged(ref _selectedRatio, value);
        }

        public IReactiveCommand AddCommand { get; }

        public AddWalletViewModel(){
            
            AddCommand = ReactiveCommand.Create<Wallet>(() => {
                return new Wallet(){
                    Name = InputName,
                    Amount = InputAmount,
                    AmountRatioId = SelectedRatio.Id
                };
            });

        }
    }
}