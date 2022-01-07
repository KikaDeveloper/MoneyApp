using System.Reactive;
using ReactiveUI;
using MoneyApp.Models;


namespace MoneyApp.ViewModels{

    public class  AddCategoryViewModel:ViewModelBase
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

        public ReactiveCommand<Unit, Category> AddCommand { get; }

        public AddCategoryViewModel(){
            AddCommand = ReactiveCommand.Create<Category>(()=>{
                return new Category(){
                    Name = InputName,
                    Amount = InputAmount
                };
            });
        }
    }
}
