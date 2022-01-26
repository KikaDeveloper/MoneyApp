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
    public partial class RecordDialogWindow : ReactiveWindow<AddRecordViewModel>
    {
        public RecordDialogWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            this.WhenActivated(d => ViewModel!.AddCommand.Subscribe(Close));
            // подписка на событие TextBox
            this.FindControl<TextBox>("Amount").AddHandler(TextInputEvent, TextInputAmountHandler, RoutingStrategies.Tunnel);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        //обработчик ввода в InputAmount
        private void TextInputAmountHandler(object? sender, TextInputEventArgs e)
        {   
            // удаление текста в input
            if(!char.IsDigit(e.Text![0]))
                e.Handled = true;
        }
    }
}