using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skillbox_HomeWork_19_MVVM.Views.Windows;
using Skillbox_HomeWork_19_MVVM.Data;
using Skillbox_HomeWork_19_MVVM.Models;

namespace Skillbox_HomeWork_19_MVVM.ViewModels
{
    public class DeleteWindowViewModel
    {
        DeleteWindow deleteWindow;
        event ActiveLog BankLog;
        DataBaseContext context;
        private object selectedClient;
        private int idSelectedClient;
        private string name;


        public object SelectedClient { get => selectedClient; set => selectedClient = value; }
        public int IdSelectedClient { get => idSelectedClient; set => idSelectedClient = value; }
        public string Name { get => name; set => name = value; }


        public DeleteWindowViewModel(Window window)
        {
            deleteWindow = window as DeleteWindow;
            BankLog += MainWindowViewModel.AddLog;
            context = new DataBaseContext();

            selectedClient = MainWindowViewModel.SelectedClient;
            GetSelectedClient();
        }

        /// <summary> Метод определяет тип выбранного клиента и инициализирует поля Имя Клиента и его Id </summary>
        public void GetSelectedClient()
        {
            if (SelectedClient.GetType() == typeof(ViewOrg))
            {
                SelectedClient = MainWindowViewModel.SelectedClient as ViewOrg;
                IdSelectedClient = (SelectedClient as ViewOrg).Id_Client;
                Name = (SelectedClient as ViewOrg).Name;
            }
            else if (SelectedClient.GetType() == typeof(ViewPerson))
            {
                SelectedClient = MainWindowViewModel.SelectedClient as ViewPerson;
                IdSelectedClient = (SelectedClient as ViewPerson).Id_Client;
                Name = (SelectedClient as ViewPerson).FirstName + " " + (SelectedClient as ViewPerson).LastName;
            }
        }

        private RelayCommand canselCommand;
        public RelayCommand CanselCommand
        {
            get
            {
                return canselCommand ??
                    (canselCommand = new RelayCommand(obj =>
                    {
                        deleteWindow.DialogResult = false;
                    }));
            }
        }

        private RelayCommand deleteCommand;
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                    (deleteCommand = new RelayCommand(obj =>
                    {
                        context.Client.Remove((Client)context.Client.Where(o => o.Id == idSelectedClient).ToList()[0]);
                        context.SaveChanges();

                        if (SelectedClient.GetType() == typeof(ViewOrg))
                        {
                            context.Organization.Remove((Organization)context.Organization.Where(o => o.id_Client == idSelectedClient).ToList()[0]);
                            context.SaveChanges();

                            MainWindowViewModel.ViewOrgs.Remove(MainWindowViewModel.ViewOrgs.Where(o => o.Id_Client == IdSelectedClient).ToList()[0]);
                        }
                        else if (SelectedClient.GetType() == typeof(ViewPerson))
                        {
                            context.NaturalPerson.Remove((NaturalPerson)context.NaturalPerson.Where(o => o.id_Client == idSelectedClient).ToList()[0]);
                            context.SaveChanges();

                            MainWindowViewModel.ViewPersons.Remove(MainWindowViewModel.ViewPersons.Where(o => o.Id_Client == IdSelectedClient).ToList()[0]);
                        }
                        BankLog?.Invoke($"{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()} клиент {Name} удалён из Базы Данных.");

                        deleteWindow.DialogResult = true;
                    }));
            }
        }


    }
}
