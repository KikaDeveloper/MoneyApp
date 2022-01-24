using ReactiveUI;
using ReactiveUI.Validation.Contexts;
using System.Reactive;
using MoneyApp.Models;

namespace MoneyApp.ViewModels
{
    public class AddRecordViewModel : ViewModelBase
    {
        #region Private variables
        
        private string? _text;
        private int _amount;
        private int _availableAmount;
        private string? _title;

        #endregion
        
        #region Public fields

        public string Title
        {
            get => _title!;
            set => this.RaiseAndSetIfChanged(ref _title, value);
        }

        public string InputText 
        {
            get => _text!;
            set => this.RaiseAndSetIfChanged(ref _text, value);
        }

        public int InputAmount
        {
            get => _amount;
            set => this.RaiseAndSetIfChanged(ref _amount, value);
        }

        public int AvailableAmount
        {
            get => _availableAmount;
            set => this.RaiseAndSetIfChanged(ref _availableAmount, value);
        }

        #endregion

        public ReactiveCommand<Unit, Record?> AddCommand { get; set; }
        public ValidationContext ValidationContext { get; } = new ValidationContext();

        public AddRecordViewModel(int availableAmount, string title){
            AvailableAmount = availableAmount;
            Title = title;

            // валидация полей ввода
            var inputsIsValid = this.WhenAnyValue(
                x => x.InputText,
                x => x.InputAmount,
                (name, amount) => {
                    return CheckInputs(name, amount);
                }
            );

            AddCommand = ReactiveCommand.Create<Record?>(()=>{
                return new Record(){
                    Text = InputText,
                    Amount = InputAmount
                };
            }, inputsIsValid);
        }

        private bool CheckInputs(string name, int amount)
        {
            bool nameIsValid = !string.IsNullOrEmpty(name);
            bool amountIsValid = amount > 0 && AvailableAmount >= amount; 

            if(nameIsValid && amountIsValid) return true;
            else return false;
        }
    }
}