using Skillbox_HomeWork_19_MVVM.ViewModels;
using System.Windows;

namespace Skillbox_HomeWork_19_MVVM.Views.Windows
{
    public partial class TransferWindow : Window
    {
        public TransferWindow()
        {
            InitializeComponent();
            DataContext = new TransferWindowViewModel(this);
        }
    }
}
