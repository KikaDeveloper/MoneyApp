using Avalonia;
using Avalonia.ReactiveUI;
using Avalonia.Markup.Xaml;
using System.Threading.Tasks;
using ReactiveUI;
using MoneyApp.Models;
using MoneyApp.ViewModels;
using MoneyApp.Dialog;

namespace MoneyApp.Views
{
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            this.WhenActivated(d => d(ViewModel!.AddWalletInteraction.RegisterHandler(AddWalletOpenDialog)));
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public async Task AddWalletOpenDialog(InteractionContext<AddWalletViewModel, Wallet?> interaction){
            var dialog = new AddWalletWindow();
            dialog.DataContext = interaction.Input;
            var result = await dialog.ShowDialog<Wallet?>(this);
            interaction.SetOutput(result);
        }
    }
}