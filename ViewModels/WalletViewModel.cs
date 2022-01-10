using ReactiveUI;
using System;
using MoneyApp.Models;

namespace MoneyApp.ViewModels
{
    public class WalletViewModel : ViewModelBase
    {
        private Wallet? _wallet;
        private CategoryManagerViewModel? _categoryMangerViewModel;

        public event EventHandler? DeleteWalletEvent;

        public Wallet Wallet
        {
            get => _wallet!;
            set => this.RaiseAndSetIfChanged(ref _wallet, value);
        }

        public CategoryManagerViewModel CategoryManagerViewModel
        {
            get => _categoryMangerViewModel!;
            set => this.RaiseAndSetIfChanged(ref _categoryMangerViewModel, value);
        }

        public IReactiveCommand? DeleteWalletCommand { get; }

        public WalletViewModel(Wallet wallet):base()
        {
            Wallet = wallet;
        }

        public WalletViewModel()
        {
            DeleteWalletCommand = ReactiveCommand.Create(()=>{
                DeleteWalletEvent?.Invoke(this, new EventArgs());
            });
        }
    }
}