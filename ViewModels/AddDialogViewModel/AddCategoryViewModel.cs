using System.Reactive;
using ReactiveUI;
using ReactiveUI.Validation.Contexts;
using MoneyApp.Models;


namespace MoneyApp.ViewModels{

    public class  AddCategoryViewModel : ViewModelBase, IDailogWindowViewModel<Category>
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

        public ReactiveCommand<Unit, Category?> AddCommand { get; }
        public ValidationContext ValidationContext { get; } = new ValidationContext();

        public AddCategoryViewModel(){
            
            // валидация полей ввода
            var dialogValid = this.WhenAnyValue(
                x => x.InputName,
                x => x.InputAmount,
                (name, amount) => {
                    if(!string.IsNullOrEmpty(name) && amount > 0)
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
