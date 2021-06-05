using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Skillbox_HomeWork_19_MVVM.Models
{
    public class ViewOrg: INotifyPropertyChanged
    {
        private int id_Client;
        private string name;
        private int employeeCount;
        private decimal deposit;
        private decimal interestRate;

        public int Id_Client { get => id_Client; set => id_Client = value; }
        public string Name { get => name; set => name = value; }
        public int EmployeeCount { get => employeeCount; set => employeeCount = value; }
        public decimal Deposit { get => deposit; set => deposit = value; }
        public decimal InterestRate { get => interestRate; set => interestRate = value; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
