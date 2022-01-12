using ReactiveUI;
using ReactiveUI.Validation.Contexts;
using System.Reactive;
using MoneyApp.Models;

namespace MoneyApp.ViewModels
{
    public class  AddRecordViewModel : ViewModelBase, IDailogWindowViewModel<Record>
    {
        private string? _text;
        private int _amount;

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

        public ReactiveCommand<Unit, Record?> AddCommand { get; }
        public ValidationContext ValidationContext { get; } = new ValidationContext();

        public AddRecordViewModel(){

             // валидация полей ввода
            var dialogValid = this.WhenAnyValue(
                x => x.InputText,
                x => x.InputAmount,
                (name, amount) => {
                    if(!string.IsNullOrEmpty(name) && amount > 0)
                        return true;
                    else return false;
                }
            );

            AddCommand = ReactiveCommand.Create<Record?>(()=>{
                return new Record(){
                    Text = InputText,
                    Amount = InputAmount
                };
            }, dialogValid);
        }
    }
}