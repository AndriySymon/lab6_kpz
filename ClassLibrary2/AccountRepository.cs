using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2
{
    public class AccountRepository
    {
        private Account[] accounts;

        public AccountRepository()
        {
            InitializeAccounts();
        }

        private void InitializeAccounts()
        {
            accounts = new Account[]
            {
                new Account("4149567890748296", "1234", "Andrew", "Symon", "ipz235_sag@student.ztu.edu.ua", "+380983279051", 10000.00, 5000.00),
                new Account("4149647389054789", "4321", "Oleksandr", "Pavlov", "ipz234_poo@student.ztu.edu.ua", "+380679806545", 5437.50, 3000.00),
                new Account("4149769307893260", "6782", "John", "Jackson", "jackson@gmail.com", "+380982789067", 1234.90, 2000.00),
                new Account("4149768990237685", "8900", "James", "Himm", "himm@gmail.com", "+380689066478", 8789.50, 8000.00),
                new Account("4149879076774893", "9212", "Tommy", "Ram", "ramtom@gmail.com", "+380986789045", 6550.29, 2000.00)
            };
        }

        public Account GetAccount(string cardNumber, string cardPIN)
        {
            return accounts.FirstOrDefault(acc => acc.CardNumber == cardNumber && acc.CardPIN == cardPIN);
        }
        public Account GetAccountByCardNumber(string cardNumber)
        {
            return accounts.FirstOrDefault(acc => acc.CardNumber == cardNumber);
        }
    }
}
