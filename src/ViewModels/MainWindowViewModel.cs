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
            Task.Run(async() => 
                {
                   var walletViewModels = await GetWalletViewModelsAsync();
                   WalletManagerViewModel = new WalletManagerViewModel(walletViewModels);
                }
            );
        }

        // получение кошельков
        private async Task<IEnumerable<WalletViewModel>> GetWalletViewModelsAsync()
        {
            MoneyRepository repo = MoneyRepository.Instance;

            var walletViewModels = new List<WalletViewModel>();
            var wallets = await repo.GetWalletsAsync();
            
            if(wallets.ToList().Count > 0)
                foreach(var wallet in wallets)
                {   
                    var categoryViewModels = await GetCategoryViewModelsAsync(repo, wallet.Id);
                    // добавление walletVM в коллецию
                    walletViewModels.Add(
                        new WalletViewModel(
                            wallet, 
                            new CategoryManagerViewModel
                            (
                                wallet.Id,
                                categoryViewModels
                            )
                        )
                    );
                }

            return walletViewModels;
        }

        // получение категорий кошелька
        private async Task<IEnumerable<CategoryViewModel>> GetCategoryViewModelsAsync(MoneyRepository repo, int walletId)
        {
            IEnumerable<Category> categories = await repo.GetCategoriesAsync(walletId);
            var categoryViewModels = new List<CategoryViewModel>();

            foreach(var category in categories)
            {
                IEnumerable<RecordViewModel> recordViewModels = await GetRecordViewModelsAsync(repo, category.Id);
                categoryViewModels.Add( new CategoryViewModel(category, recordViewModels) );
            }

            return categoryViewModels;
        }

        // получение записей конкретной категории
        private async Task<IEnumerable<RecordViewModel>> GetRecordViewModelsAsync(MoneyRepository repo, int categoryId)
        {   
            IEnumerable<Record> records = await repo.GetRecordsAsync(categoryId);
            var recordViewModels = new List<RecordViewModel>();

            foreach(var record in records)
            {
                recordViewModels.Add(new RecordViewModel(record));
            }

            return recordViewModels;
        }

    }
}
