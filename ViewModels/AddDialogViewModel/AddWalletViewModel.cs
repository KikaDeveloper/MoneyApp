using System.Reactive;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ReactiveUI;
using MoneyApp.Models;

namespace MoneyApp.ViewModels
{
    public class AddWalletViewModel : ViewModelBase
    {

        private string? _name;
        private int _amount;
        private AmountRatio _selectedRatio;

        private List<string>? _amountRatios;

        public List<string> AmountRatios
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

        public ReactiveCommand<Unit, Wallet?> AddCommand { get; }

        public AddWalletViewModel(){
            
            AmountRatios = Enum.GetNames(typeof(AmountRatio)).ToList();

            AddCommand = ReactiveCommand.Create<Wallet?>(() => {
                return new Wallet(){
                    Name = InputName,
                    Amount = InputAmount,
                    AmountRatio = SelectedRatio
                };
            });

        }
    }
}