using Skillbox_HomeWork_19_MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Skillbox_HomeWork_19_MVVM.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для ChangeDepositWindow.xaml
    /// </summary>
    public partial class ChangeDepositWindow : Window
    {
        public ChangeDepositWindow()
        {
            InitializeComponent();
            DataContext = new ChangeDepositWindowViewModel(this);
        }
    }
}
