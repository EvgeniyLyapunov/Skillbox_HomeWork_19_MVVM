using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Skillbox_HomeWork_19_MVVM.Models
{
    public class ViewPerson: INotifyPropertyChanged
    {
        private int id_Client;
        private string lastName;
        private string firstName;
        private int age;
        private decimal deposit;
        private decimal interestRate;

        public int Id_Client { get => id_Client; set => id_Client = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public int Age { get => age; set => age = value; }
        public decimal Deposit { get => deposit; set => deposit = value; }
        public decimal InterestRate { get => interestRate; set => interestRate = value; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
