using Skillbox_HomeWork_19_MVVM.ViewModels;
using System.Windows;

namespace Skillbox_HomeWork_19_MVVM.Views.Windows
{
    public partial class InterestWindow : Window
    {
        public InterestWindow()
        {
            InitializeComponent();
            DataContext = new InterestWindowViewModel(this);
        }
    }
}
