using ReactiveUI;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using MoneyApp.Models;
using MoneyApp.Services;

namespace MoneyApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private WalletManagerViewModel? _walletManagerViewModel;

        public WalletManagerViewModel WalletManagerViewModel
        {
            get => _walletManagerViewModel!;
            set => this.RaiseAndSetIfChanged(ref _walletManagerViewModel, value);
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
            
            if(wallets.ToList().Count > 0)
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
                    
                    // добавление walletVM в коллецию
                    walletViewModels.Add(new WalletViewModel(){
                        Wallet = wallet,
                        CategoryManagerViewModel = new CategoryManagerViewModel
                        (
                            wallet.Id,
                            categories_vm
                        )
                    });
                }

            WalletManagerViewModel = new WalletManagerViewModel(walletViewModels);
        }
    }
}
