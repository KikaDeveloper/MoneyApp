using ReactiveUI;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using MoneyApp.Models;
using MoneyApp.Services;

namespace MoneyApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private WalletViewModel? _walletViewModels;

        public WalletViewModel WalletViewModel
        {
            get => _walletViewModels!;
            set => this.RaiseAndSetIfChanged(ref _walletViewModels, value);
        }

        public MainWindowViewModel(){
            Task.Run(async() => await GetWalletAdapters());
        }

        private async Task GetWalletAdapters(){
            MoneyRepository repo = MoneyRepository.Instance;

            var adapters = new List<WalletAdapter>();
            var wallets = await repo.GetWalletsAsync();

            foreach(var wallet in wallets)
            {   
                var categories = await repo.GetCategoriesAsync(wallet.Id);
                var categories_vm = new List<CategoryViewModel>();
                    foreach(var category in categories)
                    {
                        var records = await repo.GetRecordsAsync(category.Id);
                        var records_vm = new List<RecordViewModel>();
                        foreach(var record in records){
                            records_vm.Add(new RecordViewModel(){
                                Record = record
                            });
                        }
                        categories_vm.Add(new CategoryViewModel(){
                            Category = category,
                            RecordViewModels = new ObservableCollection<RecordViewModel>(records_vm)
                        });
                    }
                adapters.Add(new WalletAdapter(){
                    Wallet = wallet,
                    ViewModel = new CategoryManagerViewModel(){
                        CategoryViewModels = new ObservableCollection<CategoryViewModel>(categories_vm)
                    }
                });
            }

            WalletViewModel = new WalletViewModel(adapters);
        }
    }
}
