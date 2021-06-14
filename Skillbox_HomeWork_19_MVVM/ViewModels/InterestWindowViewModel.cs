using System.Windows;
using System.Linq;
using Skillbox_HomeWork_19_MVVM.Views.Windows;
using System;
using Skillbox_HomeWork_19_MVVM.Data;

namespace Skillbox_HomeWork_19_MVVM.ViewModels
{
    public class InterestWindowViewModel
    {
        InterestWindow interestWindow;
        DataBaseContext context;
        event ActiveLog BankLog;

        public InterestWindowViewModel(Window window)
        {
            interestWindow = window as InterestWindow;
            context = new DataBaseContext();
            BankLog += MainWindowViewModel.AddLog;
        }

        private RelayCommand cancelCommand;
        public RelayCommand CancelCommand
        {
            get
            {
                return cancelCommand ??
                    (cancelCommand = new RelayCommand(obj =>
                    {
                        interestWindow.DialogResult = false;
                    }));
            }
        }

        private RelayCommand accrualOfInterestCommand;
        public RelayCommand AccrualOfInterestCommand
        {
            get
            {
                return accrualOfInterestCommand ??
                    (accrualOfInterestCommand = new RelayCommand(obj =>
                    {
                        foreach (var item in context.Client.ToList())
                        {
                            for (int i = 0; i < 12; i++)
                            {
                                if (item.capitalization == "yes")
                                {
                                    item.deposit = Math.Round(item.deposit + ((item.deposit / 100) * (item.interestRate / 12)), 0); // Число 100 - проценты; 12 - месяцы года;
                                }
                                else
                                {
                                    item.deposit = Math.Round(item.deposit + ((item.startDepositForPercents / 100) * (item.interestRate / 12)), 0);
                                }
                                if (i == 11)
                                {
                                    item.startDepositForPercents = Math.Round(item.deposit, 0);
                                }
                            }
                            context.SaveChanges();
                        }
                        BankLog?.Invoke($"{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()} Начисление процентов по всем клиентам за расчётный период - 12 месяцев.");

                        MainWindowViewModel.ViewOrgs.Clear();
                        MainWindowViewModel.ViewOrgListRefresh();
                        MainWindowViewModel.ViewPersons.Clear();
                        MainWindowViewModel.ViewPersonListRefresh();

                        interestWindow.DialogResult = true;
                    }));

            }
        }

    }
}
