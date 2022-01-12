using ReactiveUI;
using ReactiveUI.Validation.Contexts;
using Avalonia.Input;
using System;
using System.Reactive;
using MoneyApp.Models;

namespace MoneyApp.ViewModels
{
    public class  AddRecordViewModel : ViewModelBase, IDailogWindowViewModel<Record>
    {
        private string? _text;
        private int _amount;
        private string? _maxAmount;
        private IObservable<bool>? _dialogValid;
        private int _categoryRemainingAmount;

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

        public string MaxAmount
        {
            get => _maxAmount!;
            set => this.RaiseAndSetIfChanged(ref _maxAmount, value);
        }

        public ReactiveCommand<Unit, Record?> AddCommand { get; set; }
        public ValidationContext ValidationContext { get; } = new ValidationContext();

        public AddRecordViewModel(int categoryRemainingAmount){
            _categoryRemainingAmount = categoryRemainingAmount;
            MaxAmount = $"Max amount: {categoryRemainingAmount}";

            _dialogValid = this.WhenAnyValue(
                x => x.InputText,
                x => x.InputAmount,
                (name, amount) => {
                    var nameV = !string.IsNullOrEmpty(name);
                    var amountV = amount > 0 && _categoryRemainingAmount >= amount; 
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

        //обработчик ввода в InputAmount
        public void TextInputAmount(object? sender, TextInputEventArgs e)
        {   
            // удаление текста в input
            if(!char.IsDigit(e.Text![0]))
                e.Handled = true;
        }
    }
}