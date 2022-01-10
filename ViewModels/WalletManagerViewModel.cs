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
        private ObservableCollection<WalletViewModel>? _walletAdpters;
        private WalletViewModel? _selectedAdapter;

        public ObservableCollection<WalletViewModel> WalletViewModels 
        {
            get => _walletAdpters!;
            set => this.RaiseAndSetIfChanged(ref _walletAdpters, value);
        }

        public WalletViewModel SelectedAdapter
        {
            get => _selectedAdapter!;
            set => this.RaiseAndSetIfChanged(ref _selectedAdapter, value);
        }

        public IReactiveCommand? AddWalletCommand { get; set; }

        public WalletManagerViewModel(IEnumerable<WalletViewModel> WalletViewModels)
        {
            WalletViewModels = new ObservableCollection<WalletViewModel>(WalletViewModels);
            SelectedAdapter = WalletViewModels.First();

            AddWalletCommand = ReactiveCommand.CreateFromTask(async()=>{
                var result = await DialogService.ShowDialogAsync<Wallet>(
                    new AddWalletWindow(){
                        DataContext = new AddWalletViewModel()
                    }
                );
                if(result != null)
                    await InsertWallet(result);
            });
        }

        public async Task InsertWallet(Wallet wallet){
            MoneyRepository repo = MoneyRepository.Instance;
            await repo.InsertWalletAsync(wallet);
            
            WalletViewModels.Add(new WalletViewModel(){
                Wallet = wallet,
                CategoryManagerViewModel = new CategoryManagerViewModel(wallet.Id)
            });
        }
    }
}