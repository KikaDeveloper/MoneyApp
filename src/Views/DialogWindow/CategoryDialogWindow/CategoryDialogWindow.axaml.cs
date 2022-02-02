using Avalonia;
using Avalonia.Input;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;
using System;
using ReactiveUI;
using MoneyApp.ViewModels;

namespace MoneyApp.Dialog
{
    public partial class CategoryDialogWindow : ReactiveWindow<CategoryDialogViewModel>
    {
        public CategoryDialogWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            this.WhenActivated(d => ViewModel!.AddCommand.Subscribe(Close));
            this.FindControl<TextBox>("Amount").
                AddHandler(TextInputEvent, TextInputAmountHandler, RoutingStrategies.Tunnel);
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
        }
    }
}