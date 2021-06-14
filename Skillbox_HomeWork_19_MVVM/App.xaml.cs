using Skillbox_HomeWork_19_MVVM.ViewModels;
using System.Windows;

namespace Skillbox_HomeWork_19_MVVM
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindow window = new MainWindow();
            MainWindowViewModel viewModel = new MainWindowViewModel(window);
            window.DataContext = viewModel;
            window.Show();
        }
    }
}
