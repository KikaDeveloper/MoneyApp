using Avalonia;
using Avalonia.ReactiveUI;
using Avalonia.Markup.Xaml;
using MoneyApp.ViewModels;

namespace MoneyApp.Dialog
{
    public partial class AddWalletWindow : ReactiveWindow<AddWalletViewModel> 
    {
        public AddWalletWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}