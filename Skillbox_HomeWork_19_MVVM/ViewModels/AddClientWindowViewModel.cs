using Skillbox_HomeWork_19_MVVM.Data;
using Skillbox_HomeWork_19_MVVM.Models;
using Skillbox_HomeWork_19_MVVM.Views;
using Skillbox_HomeWork_19_MVVM.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Skillbox_HomeWork_19_MVVM.ViewModels
{
    public class AddClientWindowViewModel
    {
        Random rnd;
        DataBaseContext context;
        AddClientWindow addClientWindow;
        

        public AddClientWindowViewModel(Window window)
        {
            rnd = new Random();
            context = new DataBaseContext();
            addClientWindow = (window as AddClientWindow);
        }

        /// <summary>Команда закрывает окно создания Нового клиента</summary>
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

        /// <summary>Команда создаёт нового клиента типа NaturalPerson</summary>
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
                        var e = personView[personView.Count - 1];
                        ViewPerson p = new ViewPerson(e.id_Client, e.lastName, e.firstName, e.age, e.deposit, e.interestRate);

                        MainWindowViewModel.ViewPersons.Add(p);
                        MainWindowViewModel.DataGridPersonScrollToEnd();

                        addClientWindow.DialogResult = !false;
                    }));
            }
        }

        /// <summary>Команда создаёт нового клиента типа Organization</summary>
        private RelayCommand addOrganizationNewClientCommand;
        public RelayCommand AddOrganizationNewClientCommand
        {
            get
            {
                return addOrganizationNewClientCommand ??
                    (addOrganizationNewClientCommand = new RelayCommand(obj =>
                    {
                        var client = ClientCreation.GetClient(rnd);
                        context.Client.Add(client);
                        context.SaveChanges();

                        int forIdClient = context.Client.ToList()[context.Client.ToList().Count - 1].Id;

                        var organization = ClientCreation.GetOrganization(rnd, forIdClient);
                        context.Organization.Add(organization);
                        context.SaveChanges();

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

                        var e = orgView[orgView.Count - 1];
                        ViewOrg o = new ViewOrg(e.id_Client, e.name, e.employeeCount, e.deposit, e.interestRate);

                        MainWindowViewModel.ViewOrgs.Add(o);
                        MainWindowViewModel.DataGridOrgScrollToEnd();

                        addClientWindow.DialogResult = !false;
                    }));
            }
        }


    }
}
