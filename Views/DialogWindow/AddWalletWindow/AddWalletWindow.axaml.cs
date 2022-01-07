using System;
using Avalonia;
using Avalonia.ReactiveUI;
using Avalonia.Markup.Xaml;
using MoneyApp.ViewModels;
using ReactiveUI;

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
            this.WhenActivated(d => ViewModel!.AddCommand.Subscribe(Close));
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}