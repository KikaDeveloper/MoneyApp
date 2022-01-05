using ReactiveUI;
using System.Reactive;
using MoneyApp.Models;

namespace MoneyApp.ViewModels
{
    public class  AddRecordViewModel:ViewModelBase
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

        public ReactiveCommand<Unit, Record> AddCommand { get; }

        public AddRecordViewModel(){
            AddCommand = ReactiveCommand.Create<Record>(()=>{
                return new Record(){
                    Name = InputText,
                    Amount = InputAmount
                };
            });
        }
    }
}