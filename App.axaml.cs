using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using MoneyApp.Services;
using MoneyApp.ViewModels;
using MoneyApp.Views;

namespace MoneyApp
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(),
                };
                // установка родительского окна 
                DialogService.SetOwner(desktop.MainWindow);
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}