using ReactiveUI;
using Avalonia;
using Avalonia.ReactiveUI;
using Avalonia.Markup.Xaml;
using MoneyApp.ViewModels;

namespace MoneyApp.Dialog
{
    public partial class AddCategoryWindow : ReactiveWindow<AddCategoryViewModel>
    {
        public AddCategoryWindow()
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