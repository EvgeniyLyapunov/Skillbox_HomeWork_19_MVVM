using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library_RndNames;

namespace Skillbox_HomeWork_19_MVVM.Data
{
    static public class ClientCreation
    {
        /// <summary>
        /// Создание экземпляра класса Client
        /// </summary>
        /// <param name="rnd"></param>
        /// <returns>Экземпляр класса Client</returns>
        public static Client GetClient(Random rnd)
        {
            decimal tempDeposit = rnd.Next(100, 1000) * 1000;
            int tempForFlag = rnd.Next(0, 2);
            string flag = (tempForFlag == 1) ? "yes" : "no";
            return new Client
            {
                deposit = tempDeposit,
                interestRate = rnd.Next(10, 15),
                startDepositForPercents = tempDeposit,
                capitalization = flag
            };
        }

        /// <summary>
        /// Создание экземпляра класса Organization
        /// </summary>
        /// <param name="rnd"></param>
        /// <param name="forIdClient"></param>
        /// <returns>Экземпляр класса Organization</returns>
        public static Organization GetOrganization(Random rnd, int forIdClient)
        {
            return new Organization
            {
                id_Client = forIdClient,
                name = Names.OrganizationName(rnd),
                employeeCount = rnd.Next(5, 101)
            };
        }

        /// <summary>
        /// Создание экземпляра класса NaturalPerson
        /// </summary>
        /// <param name="rnd"></param>
        /// <param name="forIdClient"></param>
        /// <returns>Экземпляр класса NaturalPerson</returns>
        public static NaturalPerson GetPerson(Random rnd, int forIdClient)
        {
            return new NaturalPerson
            {
                id_Client = forIdClient,
                lastName = Names.PersonLastName(rnd),
                firstName = Names.PersonFirstName(rnd),
                age = rnd.Next(18, 80)
            };
        }
    }
}
