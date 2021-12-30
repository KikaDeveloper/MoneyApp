using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace MoneyApp.Dialog
{
    public partial class DialogWindow : Window
    {
        public DialogWindow()
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