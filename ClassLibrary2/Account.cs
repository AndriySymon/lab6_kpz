using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClassLibrary2
{
    public class Account
    {
        public delegate void AccountStateHandler(object sender, AccountEventArgs e);
        public delegate void AccountBalanceStateHandler(object sender, AccountBalanceEventArgs e);

        public event AccountStateHandler Authorizated;
        public event AccountStateHandler CheckedBalance;
        public event AccountBalanceStateHandler Added;
        public event AccountBalanceStateHandler Withdrawn;
        public event AccountBalanceStateHandler Transferred;

        public string CardNumber { get; set; }
        public string CardPIN { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public double Balance { get; set; }
        public double MaxDepositLimit { get; set; }
        public bool HasWithdrawnDeposit { get; private set; } = false;

        public Account() { }

        public Account(string cardNumber, string cardPIN, string firstName, string lastName, string email, string phone, double balance, double maxDepositLimit)
        {
            CardNumber = cardNumber;
            CardPIN = cardPIN;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Balance = balance;
            MaxDepositLimit = maxDepositLimit;
        }

        public bool ValidateAuthorization(string cardNumber, string cardPIN)
        {
            if (Regex.IsMatch(cardNumber, "^\\d{16}$") && Regex.IsMatch(cardPIN, "^\\d{4}$"))
            {
                Authorizated?.Invoke(this, new AccountEventArgs($"Авторизація успішна. Вітаємо, {FirstName}."));
                return true;
            }
            else
            {
                Authorizated?.Invoke(this, new AccountEventArgs($"Помилка введення даних. Невірний PIN або номер картки."));
                return false;
            }
        }

        public void CheckBalance()
        {
            CheckedBalance?.Invoke(this, new AccountEventArgs($"Ваш баланс {Balance} грн."));
        }
    }
}


