using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skillbox_HomeWork_19_MVVM.Views.Windows;
using Skillbox_HomeWork_19_MVVM.Models;
using Skillbox_HomeWork_19_MVVM.Data;

namespace Skillbox_HomeWork_19_MVVM.ViewModels
{
    public class ChangeDepositWindowViewModel
    {
        ChangeDepositWindow changeDepositWindow;
        event ActiveLog BankLog;
        DataBaseContext context;
        private object selectedItem;
        private int idSelectedClient;
        private string name;
        private decimal deposit;
        private string typeOperation;
        private string windowTitle;
        private string enteredAmount;


        public object SelectedItem { get => selectedItem; set => selectedItem = value; }
        public int IdSelectedClient { get => idSelectedClient; set => idSelectedClient = value; }
        public string Name { get => name; set => name = value; }
        public decimal Deposit { get => deposit; set => deposit = value; }
        public string TypeOperation { get => typeOperation; set => typeOperation = value; }
        public string WindowTitle { get => windowTitle; set => windowTitle = value; }
        public string EnteredAmount { get => enteredAmount; set => enteredAmount = value; }


        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="window">Экземпляр окна, с которым работает данная ViewModel</param>
        public ChangeDepositWindowViewModel(Window window)
        {
            changeDepositWindow = window as ChangeDepositWindow;
            BankLog += MainWindowViewModel.AddLog;
            context = new DataBaseContext();
            SelectedItem = MainWindowViewModel.SelectedClient;
            TypeOperation = MainWindowViewModel.TypeOperation;

            GetSelectedClient();
            GetTitle();
        }


        /// <summary> Метод определяет тип выбранного клиента и инициализирует поля Имя Клиента и его Депозит </summary>
        public void GetSelectedClient()
        {
            if(SelectedItem.GetType() == typeof(ViewOrg))
            {
                SelectedItem = MainWindowViewModel.SelectedClient as ViewOrg;
                IdSelectedClient = (SelectedItem as ViewOrg).Id_Client;
                Name = (SelectedItem as ViewOrg).Name;
                Deposit = (SelectedItem as ViewOrg).Deposit;
            }
            else if(SelectedItem.GetType() == typeof(ViewPerson))
            {
                SelectedItem = MainWindowViewModel.SelectedClient as ViewPerson;
                IdSelectedClient = (SelectedItem as ViewPerson).Id_Client;
                Name = (SelectedItem as ViewPerson).FirstName + " " + (SelectedItem as ViewPerson).LastName;
                Deposit = (SelectedItem as ViewPerson).Deposit;
            }
        }


        /// <summary> Метод, на основании переменной TypeOperation присваивает название окну </summary>
        public void GetTitle()
        {
            if(TypeOperation == "Increase")
            {
                WindowTitle = "Пополнение депозита";
            }
            else if(TypeOperation == "Decrease")
            {
                WindowTitle = "Вывод средств с депозита";
            }
        }


        /// <summary> Метод конвертирует введённую в текст бокс сумму операции и проверяет на ошибки ввода </summary>
        public decimal TransactionAmount()
        {
            decimal sum = 0;
            try
            {
                sum = Convert.ToDecimal(enteredAmount);
                if (TypeOperation == "Decrease" && sum > Deposit)
                {
                    sum = 0;
                    MessageBox.Show("Сумма к снятию не может быть больше суммы депозита.", "Ошибка ввода!", MessageBoxButton.OK, MessageBoxImage.Error);
                    changeDepositWindow.XXTextBoxEnterSum.Text = default;
                    return sum;
                }
            }
            catch
            {
                MessageBox.Show("Для ввода суммы воспользуйтесь цифрами.", "Ошибка ввода!", MessageBoxButton.OK, MessageBoxImage.Error);
                return sum;
            }
            return sum;
        }

        /// <summary> Команда производит операции по начислению на депозит и снятию с оного </summary>
        private RelayCommand changeDepositCommand;
        public RelayCommand ChangeDepositCommand
        {
            get
            {
                return changeDepositCommand ??
                    (changeDepositCommand = new RelayCommand(obj =>
                    {
                        decimal sumOperation = TransactionAmount();
                        if (sumOperation == 0)
                        {
                            changeDepositWindow.XXTextBoxEnterSum.Text = default;
                            return;
                        }
                        switch(TypeOperation)
                        {
                            case "Increase":
                                context.Client.Where(e => e.Id == IdSelectedClient).ToList()[0].deposit += sumOperation;
                                BankLog?.Invoke($"{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()} клиент {Name} пополнил депозит на {sumOperation}$");
                                context.SaveChanges();
                                if (SelectedItem.GetType() == typeof(ViewOrg))
                                {
                                    MainWindowViewModel.ViewOrgs.Where(o => o.Id_Client == IdSelectedClient).ToList()[0].Deposit += sumOperation;
                                }
                                else if (SelectedItem.GetType() == typeof(ViewPerson))
                                {
                                    MainWindowViewModel.ViewPersons.Where(o => o.Id_Client == IdSelectedClient).ToList()[0].Deposit += sumOperation;
                                }
                                break;

                            case "Decrease":
                                context.Client.Where(e => e.Id == IdSelectedClient).ToList()[0].deposit -= sumOperation;
                                BankLog?.Invoke($"{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()} клиент {Name} снял с депозита {sumOperation}$");
                                context.SaveChanges();
                                if (SelectedItem.GetType() == typeof(ViewOrg))
                                {
                                    MainWindowViewModel.ViewOrgs.Where(o => o.Id_Client == IdSelectedClient).ToList()[0].Deposit -= sumOperation;
                                }
                                else if (SelectedItem.GetType() == typeof(ViewPerson))
                                {
                                    MainWindowViewModel.ViewPersons.Where(o => o.Id_Client == IdSelectedClient).ToList()[0].Deposit -= sumOperation;
                                }
                                break;
                        }
                        changeDepositWindow.DialogResult = !false;
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
                        changeDepositWindow.DialogResult = false;
                    }));
            }
        }

    }
}
