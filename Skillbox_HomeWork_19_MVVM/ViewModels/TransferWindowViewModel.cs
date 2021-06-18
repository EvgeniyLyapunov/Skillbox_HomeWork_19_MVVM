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
    public class TransferWindowViewModel
    {
        TransferWindow transferWindow;
        event ActiveLog BankLog;
        DataBaseContext context;
        private object selectedSender;
        private object selectedRecipient;
        private int idSelectedSender;
        private int idSelectedRecipient;
        private string name;
        private string nameOfRecipientClient;
        private decimal senderDeposit;
        private decimal recipientDeposit;
        private string enteredAmount;
        private decimal transferAmount;
        public TransferWindowViewModel(Window window)
        {
            transferWindow = window as TransferWindow;
            BankLog += MainWindowViewModel.AddLog;
            context = new DataBaseContext();
            SelectedSender = MainWindowViewModel.SelectedClient;

            GetSender();
        }

        public object SelectedSender { get => selectedSender; set => selectedSender = value; }
        public object SelectedRecipient { get => selectedRecipient; set => selectedRecipient = value; }
        public int IdSelectedSender { get => idSelectedSender; set => idSelectedSender = value; }
        public int IdSelectedRecipient { get => idSelectedRecipient; set => idSelectedRecipient = value; }
        public string Name { get => name; set => name = value; }
        public string NameOfRecipientClient { get => nameOfRecipientClient; set => nameOfRecipientClient = value; }
        public decimal SenderDeposit { get => senderDeposit; set => senderDeposit = value; }
        public decimal RecipientDeposit { get => recipientDeposit; set => recipientDeposit = value; }
        public string EnteredAmount { get => enteredAmount; set => enteredAmount = value; }
        public decimal TransferAmount { get => transferAmount; set => transferAmount = value; }


        private RelayCommand orgRadioButtonCommand;
        public RelayCommand OrgRadioButtonCommand
        {
            get
            {
                return orgRadioButtonCommand ??
                    (orgRadioButtonCommand = new RelayCommand(obj =>
                    {
                        transferWindow.name.Visibility = Visibility.Visible;
                        transferWindow.firstName.Visibility = Visibility.Collapsed;
                        transferWindow.lastName.Visibility = Visibility.Collapsed;
                        transferWindow.XXXgridView.ItemsSource = MainWindowViewModel.ViewOrgs;
                        transferWindow.XXXgridView.SelectedItem = default;
                    }));
            }
        }

        private RelayCommand personRadioButtonCommand;
        public RelayCommand PersonRadioButtonCommand
        {
            get
            {
                return personRadioButtonCommand ??
                    (personRadioButtonCommand = new RelayCommand(obj =>
                    {
                        transferWindow.name.Visibility = Visibility.Collapsed;
                        transferWindow.firstName.Visibility = Visibility.Visible;
                        transferWindow.lastName.Visibility = Visibility.Visible;
                        transferWindow.XXXgridView.ItemsSource = MainWindowViewModel.ViewPersons;
                        transferWindow.XXXgridView.SelectedItem = default;
                    }));
            }
        }

        private RelayCommand getRecipientCommand;
        public RelayCommand GetRecipientCommand
        {
            get
            {
                return getRecipientCommand ??
                    (getRecipientCommand = new RelayCommand(obj =>
                    {
                        transferWindow.XXXOperationSum.Text = default;
                        GetRecipient();
                        transferWindow.XXXRecipient.Text = NameOfRecipientClient;
                    }));
            }
        }

        /// <summary> Команда выполняет операцию перевода </summary>
        private RelayCommand transferCommand;
        public RelayCommand TransferCommand
        {
            get
            {
                return transferCommand ??
                    (transferCommand = new RelayCommand(obj =>
                    {
                        GetSumTransfer();
                        if(TransferAmount == 0)
                        {
                            transferWindow.XXXOperationSum.Text = default;
                            return;
                        }

                        context.Client.Where(e => e.Id == IdSelectedSender).ToList()[0].deposit -= TransferAmount;
                        BankLog?.Invoke($"{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()} клиент {Name} сделал перевод клиенту {NameOfRecipientClient} на сумму {TransferAmount}$");
                        context.SaveChanges();
                        context.Client.Where(e => e.Id == IdSelectedRecipient).ToList()[0].deposit += TransferAmount;
                        BankLog?.Invoke($"{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()} депозит клиента {NameOfRecipientClient} пополнен на сумму {TransferAmount}$ переводом от клиента {Name}");
                        context.SaveChanges();

                        if (SelectedSender.GetType() == typeof(ViewOrg))
                        {
                            MainWindowViewModel.ViewOrgs.Where(o => o.Id_Client == IdSelectedSender).ToList()[0].Deposit -= TransferAmount;
                        }
                        else if (SelectedSender.GetType() == typeof(ViewPerson))
                        {
                            MainWindowViewModel.ViewPersons.Where(o => o.Id_Client == IdSelectedSender).ToList()[0].Deposit -= TransferAmount;
                        }

                        if (SelectedRecipient.GetType() == typeof(ViewOrg))
                        {
                            MainWindowViewModel.ViewOrgs.Where(o => o.Id_Client == IdSelectedRecipient).ToList()[0].Deposit += TransferAmount;
                        }
                        else if (SelectedRecipient.GetType() == typeof(ViewPerson))
                        {
                            MainWindowViewModel.ViewPersons.Where(o => o.Id_Client == IdSelectedRecipient).ToList()[0].Deposit += TransferAmount;
                        }
                        MainWindowViewModel.SelectedClient = default;
                        transferWindow.DialogResult = true;
                    }));
            }
        }

        /// <summary> Команда отменяет вызов окна </summary>
        private RelayCommand canselCommand;
        public RelayCommand CanselCommand
        {
            get
            {
                return canselCommand ??
                    (canselCommand = new RelayCommand(obj =>
                    {
                        MainWindowViewModel.SelectedClient = default;
                        transferWindow.DialogResult = false;
                    }));
            }
        }



        /// <summary> Метод определяет тип выбранного клиента - отправителя и инициализирует поля Имя Клиента и его Id </summary>
        public void GetSender()
        {
            if (SelectedSender.GetType() == typeof(ViewOrg))
            {
                SelectedSender = MainWindowViewModel.SelectedClient as ViewOrg;
                IdSelectedSender = (SelectedSender as ViewOrg).Id_Client;
                Name = (SelectedSender as ViewOrg).Name;
                SenderDeposit = (SelectedSender as ViewOrg).Deposit;
            }
            else if (SelectedSender.GetType() == typeof(ViewPerson))
            {
                SelectedSender = MainWindowViewModel.SelectedClient as ViewPerson;
                IdSelectedSender = (SelectedSender as ViewPerson).Id_Client;
                Name = (SelectedSender as ViewPerson).FirstName + " " + (SelectedSender as ViewPerson).LastName;
                SenderDeposit = (SelectedSender as ViewPerson).Deposit;
            }
        }

        /// <summary> Метод определяет тип выбранного клиента - получателя и инициализирует поля Имя Клиента и его Id </summary>
        public void GetRecipient()
        {
            if (SelectedRecipient.GetType() == typeof(ViewOrg))
            {
                SelectedRecipient = MainWindowViewModel.SelectedClient as ViewOrg;
                IdSelectedRecipient = (SelectedRecipient as ViewOrg).Id_Client;
                NameOfRecipientClient = (SelectedRecipient as ViewOrg).Name;
                RecipientDeposit = (SelectedRecipient as ViewOrg).Deposit;
            }
            else if (SelectedRecipient.GetType() == typeof(ViewPerson))
            {
                SelectedRecipient = MainWindowViewModel.SelectedClient as ViewPerson;
                IdSelectedRecipient = (SelectedRecipient as ViewPerson).Id_Client;
                NameOfRecipientClient = (SelectedRecipient as ViewPerson).FirstName + " " + (SelectedRecipient as ViewPerson).LastName;
                RecipientDeposit = (SelectedRecipient as ViewPerson).Deposit;
            }
        }

        /// <summary> Метод конвертирует введённую в текст бокс сумму операции и проверяет на ошибки ввода </summary>
        public void GetSumTransfer()
        {
            try
            {
                TransferAmount = Convert.ToDecimal(EnteredAmount);
            }
            catch
            {
                MessageBox.Show("Для ввода суммы воспользуйтесь цифрами.", "Ошибка ввода!", MessageBoxButton.OK, MessageBoxImage.Error);
                TransferAmount = 0;
                return;
            }
            if (TransferAmount > SenderDeposit)
            {
                MessageBox.Show("Сумма перевода не может быть больше суммы стартового депозита.", "Ошибка ввода!", MessageBoxButton.OK, MessageBoxImage.Error);
                TransferAmount = 0;
                return;
            }
            else if (IdSelectedSender == IdSelectedRecipient)
            {
                MessageBox.Show("Клиент отправитель не может быть клиентом получателем!", "Ошибка ввода!", MessageBoxButton.OK, MessageBoxImage.Error);
                TransferAmount = 0;
                return;
            }
        }


    }
}
