using System.Collections.Generic;
using System.Collections.ObjectModel;
using ReactiveUI;
using MoneyApp.Models;

namespace MoneyApp.ViewModels
{
    public class WalletViewModel : ViewModelBase
    {
        private ObservableCollection<WalletAdapter>? _walletAdpters;

        public ObservableCollection<WalletAdapter> WalletAdapters 
        {
            get => _walletAdpters!;
            set => this.RaiseAndSetIfChanged(ref _walletAdpters, value);
        }

        public WalletViewModel(IEnumerable<WalletAdapter> walletAdapters){
            WalletAdapters = new ObservableCollection<WalletAdapter>(walletAdapters);
        }
    }
}