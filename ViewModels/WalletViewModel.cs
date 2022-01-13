using ReactiveUI;
using System;
using System.Collections.Specialized;
using MoneyApp.Models;

namespace MoneyApp.ViewModels
{
    public class WalletViewModel : ViewModelBase
    {
        private Wallet? _wallet;
        private CategoryManagerViewModel? _categoryManagerViewModel;
        private int _remainingAmount;
        public event EventHandler? DeleteWalletEvent;

        public int AvailableAmount
        {
            get => _remainingAmount;
            set => this.RaiseAndSetIfChanged(ref _remainingAmount, value);
        }

        public Wallet Wallet
        {
            get => _wallet!;
            set => this.RaiseAndSetIfChanged(ref _wallet, value);
        }

        public CategoryManagerViewModel CategoryManagerViewModel
        {
            get => _categoryManagerViewModel!;
            set => this.RaiseAndSetIfChanged(ref _categoryManagerViewModel, value);
        }

        public IReactiveCommand? DeleteWalletCommand { get; }

        public WalletViewModel(Wallet wallet ,CategoryManagerViewModel categoryManagerViewModel)
        {
            Wallet = wallet;
            CategoryManagerViewModel = categoryManagerViewModel;
            CategoryManagerViewModel.CategoryViewModels.CollectionChanged += CategoryViewModelsCollectionChanged;

            UpdateRemainingAmount();

            DeleteWalletCommand = ReactiveCommand.Create(()=>{
                DeleteWalletEvent?.Invoke(this, new EventArgs());
            });
        }

        private void UpdateRemainingAmount()
        {
            int remainingAmount = Wallet.Amount;
            foreach(var categoryVM in CategoryManagerViewModel.CategoryViewModels)
                remainingAmount -= categoryVM.Category.Amount;
            AvailableAmount = remainingAmount;
        }

        private void CategoryViewModelsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
            => UpdateRemainingAmount();
    }
}