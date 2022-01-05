using Avalonia;
using Avalonia.ReactiveUI;
using Avalonia.Markup.Xaml;
using MoneyApp.ViewModels;

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
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}