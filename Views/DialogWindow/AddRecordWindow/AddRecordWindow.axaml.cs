using System;
using Avalonia;
using Avalonia.ReactiveUI;
using Avalonia.Markup.Xaml;
using MoneyApp.ViewModels;
using ReactiveUI;

namespace MoneyApp.Dialog
{
    public partial class AddRecordWindow : ReactiveWindow<AddRecordViewModel>
    {
        public AddRecordWindow()
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