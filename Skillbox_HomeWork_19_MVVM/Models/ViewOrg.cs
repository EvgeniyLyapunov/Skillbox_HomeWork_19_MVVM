using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace Skillbox_HomeWork_19_MVVM.Models
{
    public class ViewOrg: INotifyPropertyChanged
    {
        public ViewOrg(int Id_Client, string Name, int EmployeeCount, decimal Deposit, decimal InterestRate)
        {
            id_Client = Id_Client;
            name = Name;
            employeeCount = EmployeeCount;
            deposit = Deposit;
            interestRate = InterestRate;
        }

        private int id_Client;
        private string name;
        private int employeeCount;
        private decimal deposit;
        private decimal interestRate;

        public int Id_Client
        {
            get => id_Client;
            set
            {
                id_Client = value;
                OnPropertyChanged("Id_Client");
            }
        }
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public int EmployeeCount
        {
            get => employeeCount;
            set
            {
                employeeCount = value;
                OnPropertyChanged("EmployeeCount");
            }
        }
        public decimal Deposit
        {
            get => deposit;
            set
            {
                deposit = value;
                OnPropertyChanged("Deposit");
            }
        }
        public decimal InterestRate
        {
            get => interestRate;
            set
            {
                interestRate = value;
                OnPropertyChanged("InterestRate");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
