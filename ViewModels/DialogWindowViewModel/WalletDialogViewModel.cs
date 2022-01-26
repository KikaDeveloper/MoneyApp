using System;
using System.Linq;
using System.Reactive;
using System.Collections.Generic;
using ReactiveUI;
using ReactiveUI.Validation.Contexts;
using MoneyApp.Models;

namespace MoneyApp.ViewModels
{
    public class WalletDialogViewModel : ViewModelBase
    {

        #region Private variables

        private string? _name;
        private int _amount;
        private string? _selectedRatio;
        private List<string>? _amountRatios;
        private string? _title;
        
        #endregion
        
        #region Public fields

        public string Title
        {
            get => _title!;
            set => this.RaiseAndSetIfChanged(ref _title, value);
        }

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

        public string SelectedRatio
        {
            get => _selectedRatio!;
            set => this.RaiseAndSetIfChanged(ref _selectedRatio, value);
        }

        #endregion

        public ReactiveCommand<Unit, Wallet?> AddCommand { get; }

        public ValidationContext ValidationContext { get; } = new ValidationContext();

        public WalletDialogViewModel(string title){ 
            Title = title;
            AmountRatios = Enum.GetNames(typeof(AmountRatio)).ToList();
            SelectedRatio = AmountRatios.First();

            // валидация полей ввода
            var inputsIsValid = this.WhenAnyValue(
                x => x.InputName,
                x => x.InputAmount,
                x => x.SelectedRatio,
                (name, amount, ratio) => {
                    return CheckInputs(name, amount, ratio);
                }
            );

            AddCommand = ReactiveCommand.Create<Wallet?>(() => {
                return new Wallet(){
                    Name = InputName,
                    Amount = InputAmount,
                    AmountRatio = (AmountRatio)Enum.Parse(typeof(AmountRatio), SelectedRatio)
                };
            }, inputsIsValid);
        }

        private bool CheckInputs(string name, int amount, string ratio)
        {
            bool nameIsValid = !string.IsNullOrEmpty(name);
            bool amountIsValid = amount > 0;
            bool ratioIsValid = ratio != null;
            
            if(nameIsValid && amountIsValid && ratioIsValid) return true;
            else return false;
        }
    }
}