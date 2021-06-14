using Skillbox_HomeWork_19_MVVM.Models;
using Skillbox_HomeWork_19_MVVM.Views.Windows;
using System;
using System.Linq;
using System.Windows;
using Skillbox_HomeWork_19_MVVM.Data;
using System.Collections.ObjectModel;

namespace Skillbox_HomeWork_19_MVVM.ViewModels
{
    public delegate void ActiveLog(string arg);
    public class MainWindowViewModel
    {
        private static ObservableCollection<ViewOrg> viewOrgs;
        private static ObservableCollection<ViewPerson> viewPersons;
        private static ObservableCollection<string> log;
        private static MainWindow mainWindow;


        public MainWindowViewModel(Window window)
        {
            mainWindow = window as MainWindow;
            ViewOrgListRefresh();
            ViewPersonListRefresh();
        }

        static MainWindowViewModel()
        {
            viewOrgs = new ObservableCollection<ViewOrg>();
            viewPersons = new ObservableCollection<ViewPerson>();
            log = new ObservableCollection<string>();
        }

        public static ObservableCollection<ViewOrg> ViewOrgs { get => viewOrgs; set => viewOrgs = value; }
        public static ObservableCollection<ViewPerson> ViewPersons { get => viewPersons; set => viewPersons = value; }
        public static ObservableCollection<string> Log { get => log; set => log = value; }

        #region// Команды для кнопок и меню

        /// <summary>Команда открывает окно по созданию нового клиента</summary>
        private RelayCommand addNewClientWindowCommand;
        public RelayCommand AddNewClientWindowCommand
        {
            get
            {
                return addNewClientWindowCommand ??
                   (addNewClientWindowCommand = new RelayCommand(obj =>
                   {
                       AddClientWindow addClientWindow = new AddClientWindow();
                       addClientWindow.ShowDialog();
                   }));
            }
        }

        /// <summary>Команда открывает окно по "начислению" процентов</summary>
        private RelayCommand accrualOfInterestCommand;
        public RelayCommand AccrualOfInterestCommand
        {
            get
            {
                return accrualOfInterestCommand ??
                    (accrualOfInterestCommand = new RelayCommand(obj =>
                    {
                        InterestWindow interestWindow = new InterestWindow();
                        interestWindow.ShowDialog();
                    }));
            }
        }

        /// <summary>Команда закрывает приложение</summary>
        private RelayCommand exitCommand;
        public RelayCommand ExitCommand
        {
            get
            {
                return exitCommand ??
                    (exitCommand = new RelayCommand(obj =>
                    {
                        Application.Current.Shutdown();
                    }));
            }
        }

        #endregion

        /// <summary>Метод формирует список объединённых данных, инициализирует экземпляры ViewOrg и добавляет их в коллекцию ViewOrgs</summary>
        public static void ViewOrgListRefresh()
        {
            DataBaseContext context = new DataBaseContext();
            var orgView = context.Organization.Join(context.Client,
               org => org.id_Client,
               _client => _client.Id,
               (org, _client) => new
               {
                   org.id_Client,
                   org.name,
                   org.employeeCount,
                   _client.deposit,
                   _client.interestRate
               }).ToList();

            foreach (var e in orgView)
            {
                ViewOrg org = new ViewOrg(e.id_Client, e.name, e.employeeCount, e.deposit, e.interestRate);
                ViewOrgs.Add(org);
            }
        }

        /// <summary>Метод формирует список объединённых данных, инициализирует экземпляры ViewPerson и добавляет их в коллекцию ViewPersons</summary>
        public static void ViewPersonListRefresh()
        {
            DataBaseContext context = new DataBaseContext();
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

        /// <summary>Метод прокручивает в поле видимости последний элемент DataGridPerson</summary>
        public static void DataGridPersonScrollToEnd()
        {
            if (mainWindow.XDataGridPerson.Items.Count == 0)
                return;

            int index = mainWindow.XDataGridPerson.Items.Count - 1;
            object item = mainWindow.XDataGridPerson.Items[index];

            mainWindow.XDataGridPerson.ScrollIntoView(item);
        }

        /// <summary>Метод прокручивает в поле видимости последний элемент DataGridOrg</summary>
        public static void DataGridOrgScrollToEnd()
        {
            if (mainWindow.XDataGridOrg.Items.Count == 0)
                return;

            int index = mainWindow.XDataGridOrg.Items.Count - 1;
            object item = mainWindow.XDataGridOrg.Items[index];

            mainWindow.XDataGridOrg.ScrollIntoView(item);
        }

        /// <summary>
        /// Метод, используемый для подписки на событие BankLog, добавляет сообщение о операции с депозитом клиента в коллекцию log
        /// </summary>
        /// <param name="arg">Сообщение о банковской операции</param>
        public static void AddLog(string arg)
        {
            Log.Add(arg);
        }


    }
}
