using ReactiveUI;
using ReactiveUI.Validation.Contexts;
using ReactiveUI.Validation.Abstractions;
using System.Reactive;

namespace MoneyApp.ViewModels
{
    public interface IDailogWindowViewModel<TCommandOutput> : IValidatableViewModel
    {
        new ValidationContext ValidationContext { get; }
        ReactiveCommand<Unit, TCommandOutput?> AddCommand { get; }
    }
}