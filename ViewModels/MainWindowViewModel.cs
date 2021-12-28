using ReactiveUI;

namespace MoneyApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private WalletViewModel? _walletViewModels;

        public WalletViewModel WalletViewModels
        {
            get => _walletViewModels!;
            set => this.RaiseAndSetIfChanged(ref _walletViewModels, value);
        }

        public MainWindowViewModel(){

        }
    }
}
