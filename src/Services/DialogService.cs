using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls;
using System.Threading.Tasks;

namespace MoneyApp.Services
{
    public static class DialogService
    {
        private static Window? _owner;

        public static void SetOwner(ref IClassicDesktopStyleApplicationLifetime owner) 
            => _owner = owner.MainWindow;

        public static async Task<TOutput> ShowDialogAsync<TOutput>(Window dialog) 
            => await dialog.ShowDialog<TOutput>(_owner);
    }
}