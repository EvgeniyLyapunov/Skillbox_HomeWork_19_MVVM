using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Collections;

namespace Skillbox_HomeWork_19_MVVM.Models
{
    public class ViewPerson: INotifyPropertyChanged
    {
        public ViewPerson(int Id_Client, string LastName, string FirstName, int Age, decimal Deposit, decimal InterestRate)
        {
            id_Client = Id_Client;
            lastName = LastName;
            firstName = FirstName;
            age = Age;
            deposit = Deposit;
            interestRate = InterestRate;
        }

        private int id_Client;
        private string lastName;
        private string firstName;
        private int age;
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
        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                OnPropertyChanged("LastName");
            }
        }

        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }
        public int Age
        {
            get => age;
            set
            {
                age = value;
                OnPropertyChanged("Age");
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
