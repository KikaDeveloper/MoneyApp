using ReactiveUI;
using ReactiveUI.Validation.Contexts;
using System.Reactive;
using MoneyApp.Models;

namespace MoneyApp.ViewModels
{
    public class AddRecordViewModel : ViewModelBase
    {
        private string? _text;
        private int _amount;
        private int _availableAmount;
        private string? _title;

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

        public ReactiveCommand<Unit, Record?> AddCommand { get; set; }
        public ValidationContext ValidationContext { get; } = new ValidationContext();

        public AddRecordViewModel(int availableAmount, string title){
            AvailableAmount = availableAmount;
            Title = title;

            // валидация полей ввода
            var _dialogValid = this.WhenAnyValue(
                x => x.InputText,
                x => x.InputAmount,
                (name, amount) => {
                    var nameV = !string.IsNullOrEmpty(name);
                    var amountV = amount > 0 && AvailableAmount >= amount; 
                    if(nameV && amountV)
                        return true;
                    else return false;
                }
            );

            AddCommand = ReactiveCommand.Create<Record?>(()=>{
                return new Record(){
                    Text = InputText,
                    Amount = InputAmount
                };
            }, _dialogValid);
        
        }
    }
}