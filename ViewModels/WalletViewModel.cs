using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ReactiveUI;
using MoneyApp.ViewModels;
using MoneyApp.Dialog;
using MoneyApp.Models;
using MoneyApp.Services;

namespace MoneyApp.ViewModels
{
    public class WalletViewModel : ViewModelBase
    {
        private ObservableCollection<WalletAdapter>? _walletAdpters;
        private WalletAdapter? _selectedAdapter;

        public ObservableCollection<WalletAdapter> WalletAdapters 
        {
            get => _walletAdpters!;
            set => this.RaiseAndSetIfChanged(ref _walletAdpters, value);
        }

        public WalletAdapter SelectedAdapter
        {
            get => _selectedAdapter!;
            set => this.RaiseAndSetIfChanged(ref _selectedAdapter, value);
        }

        public IReactiveCommand? AddWalletCommand { get; set; }

        public WalletViewModel(IEnumerable<WalletAdapter> walletAdapters){
            WalletAdapters = new ObservableCollection<WalletAdapter>(walletAdapters);
            SelectedAdapter = WalletAdapters.First();
            AddWalletCommand = ReactiveCommand.CreateFromTask(async()=>{
                var result = await DialogService.ShowDialogAsync<Wallet>(
                    new AddWalletWindow(){
                        DataContext = new AddWalletViewModel()
                    }
                );
            });
        }
    }
}