using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ReactiveUI;
using MoneyApp.Dialog;
using MoneyApp.Models;
using MoneyApp.Services;

namespace MoneyApp.ViewModels
{
    public class WalletManagerViewModel : ViewModelBase
    {
        private ObservableCollection<WalletViewModel>? _walletViewModels;
        private WalletViewModel? _selectedWalletViewModel;

        public ObservableCollection<WalletViewModel> WalletViewModels 
        {
            get => _walletViewModels!;
            set => this.RaiseAndSetIfChanged(ref _walletViewModels, value);
        }

        public WalletViewModel SelectedWalletViewModel
        {
            get => _selectedWalletViewModel!;
            set => this.RaiseAndSetIfChanged(ref _selectedWalletViewModel, value);
        }

        public IReactiveCommand? OpenDialogCommand { get; set; }

        public WalletManagerViewModel(IEnumerable<WalletViewModel> WalletViewModels)
        {
            this.WalletViewModels = new ObservableCollection<WalletViewModel>(WalletViewModels);
     
            // подписка на событие удаления кошелька
            if(this.WalletViewModels.Count > 0)
                foreach(var walletVM in this.WalletViewModels)
                    walletVM.DeleteWalletEvent += DeleteWalletEventHandler;

            SelectedWalletViewModel = this.WalletViewModels.FirstOrDefault()!;

            OpenDialogCommand = ReactiveCommand.CreateFromTask(async()=>{
                var result = await DialogService.ShowDialogAsync<Wallet>(
                    new AddWalletWindow(){
                        DataContext = new AddWalletViewModel("New Wallet")
                    }
                );
                if(result != null)
                    await InsertWallet(result);
            });
        }

        // добавление кошелька и его viewmodel
        public async Task InsertWallet(Wallet wallet){
            MoneyRepository repo = MoneyRepository.Instance;
            await repo.InsertWalletAsync(wallet);
            
            var newWallet = new WalletViewModel(
                wallet, 
                new CategoryManagerViewModel
                (
                    wallet.Id,
                    new ObservableCollection<CategoryViewModel>()
                )
            );
            newWallet.DeleteWalletEvent += DeleteWalletEventHandler;
            WalletViewModels.Add(newWallet);
        }

        // удаление кошелька и его viewmodel из бд
        public void DeleteWalletEventHandler(object? sender, EventArgs e){
            var vm = (WalletViewModel)sender!;
            WalletViewModels.Remove(vm);
    
            Task.Run(async()=>{
                MoneyRepository repo = MoneyRepository.Instance;
                await repo.DeleteWalletAsync(vm.Wallet);
            });
        }
    }
}