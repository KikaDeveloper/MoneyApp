using System.Reactive;
using ReactiveUI;
using ReactiveUI.Validation.Contexts;
using MoneyApp.Models;


namespace MoneyApp.ViewModels{

    public class  AddCategoryViewModel : ViewModelBase
    {
        private string? _name;
        private int _amount;
        private int _availableAmount;
        private string? _title;

        public string Title
        {
            get => _title!;
            set => this.RaiseAndSetIfChanged(ref _title, value);
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

        public int AvailableAmount
        {
            get => _availableAmount;
            set => this.RaiseAndSetIfChanged(ref _availableAmount, value);
        }

        public ReactiveCommand<Unit, Category?> AddCommand { get; }
        public ValidationContext ValidationContext { get; } = new ValidationContext();

        public AddCategoryViewModel(int availableAmount, string title){
            AvailableAmount = availableAmount;
            Title = title;

            // валидация полей ввода
            var dialogValid = this.WhenAnyValue(
                x => x.InputName,
                x => x.InputAmount,
                (name, amount) => {
                    var nameV = !string.IsNullOrEmpty(name);
                    var amountV = amount > 0 && AvailableAmount >= amount; 
                    if(nameV && amountV)
                        return true;
                    else return false;
                }
            );
            
            AddCommand = ReactiveCommand.Create<Category?>(()=>{
                return new Category(){
                    Name = InputName,
                    Amount = InputAmount
                };
            }, dialogValid);
        }
    }
}
