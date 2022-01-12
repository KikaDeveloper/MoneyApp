using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
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

            // подписка на событие TextBox
            this.WhenActivated(
                d => this.FindControl<TextBox>("Amount")
                .AddHandler(TextInputEvent, this.ViewModel!.TextInputAmount, RoutingStrategies.Tunnel)
            );
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}