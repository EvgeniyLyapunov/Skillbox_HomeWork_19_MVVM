using Skillbox_HomeWork_19_MVVM.ViewModels;
using System.Windows;

namespace Skillbox_HomeWork_19_MVVM.Views.Windows
{
    public partial class AddClientWindow : Window
    {
        public AddClientWindow()
        {
            InitializeComponent();
            DataContext = new AddClientWindowViewModel(this);
        }
    }
}
