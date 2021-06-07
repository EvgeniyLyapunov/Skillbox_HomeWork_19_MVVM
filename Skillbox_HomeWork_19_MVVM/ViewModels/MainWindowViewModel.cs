using Skillbox_HomeWork_19_MVVM.Models;
using Skillbox_HomeWork_19_MVVM.Views.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Skillbox_HomeWork_19_MVVM.Data;
using System.Data.Entity;

namespace Skillbox_HomeWork_19_MVVM.ViewModels
{
    public class MainWindowViewModel: INotifyPropertyChanged
    {
        DataBaseContext context;
        Random rnd;
        private List<ViewOrg> viewOrgs;
        private List<ViewPerson> viewPersons;

        public static AddClientWindow addClientWindow;


        public MainWindowViewModel()
        {
            context = new DataBaseContext();
            rnd = new Random();
            viewOrgs = new List<ViewOrg>();
            viewPersons = new List<ViewPerson>();

            ViewPersonListRefresh();
        }
        
        public List<ViewOrg> ViewOrgs
        {
            get => viewOrgs;
            set
            {
                viewOrgs = value;
                OnPropertyChanged("ViewOrgs");
            }
        }

        public List<ViewPerson> ViewPersons
        {
            get => viewPersons;
            set
            {
                viewPersons = value;
                OnPropertyChanged("ViewPersons");
            }
        }

        #region// Команды для кнопок и меню

        /// <summary>
        /// Команда открывает окно по зозданию нового клиента
        /// </summary>
        private RelayCommand addNewClientWindowCommand;
        public RelayCommand AddNewClientWindowCommand
        {
            get
            {
                return addNewClientWindowCommand ??
                   (addNewClientWindowCommand = new RelayCommand(obj =>
                   {
                       addClientWindow = new AddClientWindow();
                       addClientWindow.ShowDialog();
                   }));
            }
        }

        /// <summary>
        /// Команда закрывает окно создания Нового клиента
        /// </summary>
        private RelayCommand cancelAddNewClientCommand;
        public RelayCommand CancelAddNewClientCommand
        {
            get
            {
                return cancelAddNewClientCommand ??
                    (cancelAddNewClientCommand = new RelayCommand(obj =>
                    {
                        addClientWindow.DialogResult = false;
                    }));
            }
        }

        /// <summary>
        /// Команда создаёт нового клиента типа NaturalPerson
        /// </summary>

        private RelayCommand addNaturalPersonNewClientCommand;
        public RelayCommand AddNaturalPersonNewClientCommand
        {
            get
            {
                return addNaturalPersonNewClientCommand ??
                    (addNaturalPersonNewClientCommand = new RelayCommand(obj =>
                    {
                        var client = ClientCreation.GetClient(rnd);
                        context.Client.Add(client);
                        context.SaveChanges();

                        int forIdClient = context.Client.ToList()[context.Client.ToList().Count - 1].Id;

                        var naturalPerson = ClientCreation.GetPerson(rnd, forIdClient);
                        context.NaturalPerson.Add(naturalPerson);
                        context.SaveChanges();

                        ViewPersonListRefresh();

                        addClientWindow.DialogResult = !false;
                    }));
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        /// <summary>
        /// Метод формирует список объединённых данных, инициализирует экземпляры ViewPerson и добавляет их в коллекцию ViewPersons
        /// </summary>
        public void ViewPersonListRefresh()
        {
            var personView = context.NaturalPerson.Join(context.Client,
               person => person.id_Client,
               _client => _client.Id,
               (person, _client) => new
               {
                   person.id_Client,
                   person.lastName,
                   person.firstName,
                   person.age,
                   _client.deposit,
                   _client.interestRate
               }).ToList();

            foreach (var e in personView)
            {
                ViewPerson person = new ViewPerson(e.id_Client, e.lastName, e.firstName, e.age, e.deposit, e.interestRate);
                ViewPersons.Add(person);
            }
        }
    }
}
