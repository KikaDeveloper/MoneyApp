using ReactiveUI;
using System;
using System.Collections.Specialized;
using MoneyApp.Models;

namespace MoneyApp.ViewModels
{
    public class WalletViewModel : ViewModelBase
    {

        #region Private variables

        private Wallet? _wallet;
        private CategoryManagerViewModel? _categoryManagerViewModel;
        private int _remainingAmount;

        #endregion

        #region Public fields

        public int AvailableAmount
        {
            get => _remainingAmount;
            set {
                this.RaiseAndSetIfChanged(ref _remainingAmount, value);
                CategoryManagerViewModel.WalletAvailableAmount = value;
            }
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

        #endregion

        public event EventHandler? DeleteWalletEvent;
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