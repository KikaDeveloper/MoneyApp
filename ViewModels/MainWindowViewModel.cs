﻿using ReactiveUI;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using MoneyApp.Models;
using MoneyApp.Services;

namespace MoneyApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private WalletManagerViewModel? _walletViewModels;

        public WalletManagerViewModel WalletViewModel
        {
            get => _walletViewModels!;
            set => this.RaiseAndSetIfChanged(ref _walletViewModels, value);
        }

        public MainWindowViewModel()
        {
            Task.Run(async() => await GetWalletViewModels());
        }

        private async Task GetWalletViewModels()
        {
            MoneyRepository repo = MoneyRepository.Instance;

            var walletViewModels = new List<WalletViewModel>();
            var wallets = await repo.GetWalletsAsync();

            foreach(var wallet in wallets)
            {   
                // получение категорий кошелька
                var categories = await repo.GetCategoriesAsync(wallet.Id);
                var categories_vm = new List<CategoryViewModel>();

                foreach(var category in categories)
                {
                    // получение записей конкретной категории
                    var records = await repo.GetRecordsAsync(category.Id);
                    var records_vm = new List<RecordViewModel>();

                    foreach(var record in records)
                    {
                        records_vm.Add(new RecordViewModel(record));
                    }

                    categories_vm.Add(new CategoryViewModel
                    (
                        category, 
                        new List<RecordViewModel>(records_vm)
                    ));
                }
                 
                // добавление адаптера в коллецию
                walletViewModels.Add(new WalletViewModel(){
                    Wallet = wallet,
                    CategoryManagerViewModel = new CategoryManagerViewModel
                    (
                        wallet.Id,
                        categories_vm
                    )
                });
            }

            WalletViewModel = new WalletManagerViewModel(walletViewModels);
        }
    }
}
