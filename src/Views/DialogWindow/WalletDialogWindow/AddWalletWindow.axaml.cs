using System;
using Avalonia;
using Avalonia.Input;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;
using MoneyApp.ViewModels;
using MoneyApp.Converters;
using ReactiveUI;


namespace MoneyApp.Dialog
{
    public partial class WalletDialogWindow : ReactiveWindow<WalletDialogViewModel> 
    {
        public WalletDialogWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            this.WhenActivated(d => ViewModel!.AddCommand.Subscribe(Close));

            this.FindControl<TextBox>("Amount").AddHandler(TextInputEvent, TextInputAmountHandler, RoutingStrategies.Tunnel);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        //обработчик ввода в InputAmount
        public void TextInputAmountHandler(object? sender, TextInputEventArgs e)
        {   
            // удаление текста в input
            if(!char.IsDigit(e.Text![0]))
                e.Handled = true;

            // var converter = new AmountConverter(); 
            // e.Text = converter.ConvertToString(e.Text);
            // e.Handled = true;
        }
    }
}