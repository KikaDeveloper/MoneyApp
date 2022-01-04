using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ReactiveUI;
using MoneyApp.Models;

namespace MoneyApp.ViewModels
{
    public class WalletViewModel : ViewModelBase
    {
        private ObservableCollection<WalletAdapter>? _walletAdpters;
        private ReadOnlyCollection<AmountRatio>? _amountRatios;
        private WalletAdapter? _selectedAdapter;

        public ObservableCollection<WalletAdapter> WalletAdapters 
        {
            get => _walletAdpters!;
            set => this.RaiseAndSetIfChanged(ref _walletAdpters, value);
        }

        public ReadOnlyCollection<AmountRatio> AmountRatios
        {
            get => _amountRatios!;
            set => this.RaiseAndSetIfChanged(ref _amountRatios, value);
        }

        public WalletAdapter SelectedAdapter
        {
            get => _selectedAdapter!;
            set => this.RaiseAndSetIfChanged(ref _selectedAdapter, value);
        }

        public IReactiveCommand? AddWalletCommand { get; set; }

        public WalletViewModel(IEnumerable<WalletAdapter> walletAdapters, IList<AmountRatio> amountRatios){
            WalletAdapters = new ObservableCollection<WalletAdapter>(walletAdapters);
            AmountRatios = new ReadOnlyCollection<AmountRatio>(amountRatios);
            SelectedAdapter = WalletAdapters.First();
        }
    }
}