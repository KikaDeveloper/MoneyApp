using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace MoneyApp.Dialog
{
    public partial class AddCategoryWindow : Window
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