using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassLibrary2;

namespace WindowsFormsApp
{
    public partial class AutomatedTellerMachineForm : Form
    {
        private Account account;
        private Account[] accounts;
        private AccountRepository accountRepository;
        private Account currentAccount;

        private double pendingDepositAmount = 0.0;
        private bool awaitingCash = false;
        public AutomatedTellerMachineForm( Account account)
        {
            InitializeComponent();
            currentAccount = account;
            InitializeBlinkingLabel();

            this.account = account;

            account.CheckedBalance += OnCheckedBalance;
            account.Added += OnMoneyAdded;
            account.Withdrawn += OnMoneyWithdrawn;
            account.Transferred += OnMoneyTransferred;

            accountRepository = new AccountRepository();
        }

        private void AutomatedTellerMachineForm_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            this.KeyDown += AutomatedTellerMachineForm_KeyDown;
        }

        private void btnCheckBalance_Click(object sender, EventArgs e)
        {
            account.CheckBalance();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            AuthorizationForm loginForm = new AuthorizationForm();
            loginForm.StartPosition = FormStartPosition.CenterScreen;
            loginForm.Show();
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            string targetCardNumber = txtTransferCardNumber.Text;

            if (double.TryParse(txtTransferAmount.Text, out double transferAmount))
            {
                Account targetAccount = accountRepository.GetAccountByCardNumber(targetCardNumber);

                if (targetAccount != null)
                {
                    account.Transfer(transferAmount, targetAccount);
                    txtTransferAmount.Clear();
                    txtTransferCardNumber.Clear();
                }
                else
                {
                    MessageBox.Show("Карта призначення не знайдена.");
                }
            }
            else
            {
                MessageBox.Show("Введіть коректну суму.");
            }
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtWithdrawAmount.Text, out double withdrawAmount))
            {
                if (withdrawAmount <= 0)
                {
                    MessageBox.Show("Сума повинна бути більшою за 0.");
                    return;
                }

                if (withdrawAmount > account.Balance)
                {
                    MessageBox.Show("На рахунку недостатньо коштів.");
                    return;
                }

                account.Balance -= withdrawAmount;

                AccountRepository repo = new AccountRepository();
                bool updated = repo.UpdateAccount(account);

                if (updated)
                {
                    MessageBox.Show("Готівку знято. Новий баланс: " + account.Balance.ToString("0.00"));
                    txtWithdrawAmount.Clear();
                }
                else
                {
                    MessageBox.Show("Помилка при оновленні балансу.");
                }
            }
            else
            {
                MessageBox.Show("Введіть коректну суму.");
            }
        }

        private void OnCheckedBalance(object sender, AccountEventArgs e)
        {
            MessageBox.Show(e.Message); 
        }

        private void OnMoneyAdded(object sender, AccountBalanceEventArgs e)
        {
            MessageBox.Show($"{e.Message} Ваш баланс: {account.Balance} грн.");  
        }

        private void OnMoneyWithdrawn(object sender, AccountBalanceEventArgs e)
        {
            MessageBox.Show($"{e.Message} Ваш баланс: {account.Balance} грн."); 
        }

        private void OnMoneyTransferred(object sender, AccountBalanceEventArgs e)
        {
            MessageBox.Show($"{e.Message} Ваш баланс: {account.Balance} грн.");  
        }

        private Timer blinkTimer;
        private bool isLabelVisible = true;
        private void InitializeBlinkingLabel()
        {
            blinkTimer = new Timer();
            blinkTimer.Interval = 2000;
            blinkTimer.Tick += new EventHandler(BlinkLabel);
            blinkTimer.Start();
        }
        private void BlinkLabel(object sender, EventArgs e)
        {
            isLabelVisible = !isLabelVisible;
            attention.Visible = isLabelVisible;
        }

        private void btnManageAccounts_Click(object sender, EventArgs e)
        {
            var manageForm = new AccountManagementForm(currentAccount);
            manageForm.ShowDialog();
        }

        private void attention_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void btnCash_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtCashAmount.Text, out double amount) && amount > 0)
            {
                pendingDepositAmount = amount;
                awaitingCash = true;
                MessageBox.Show("Очікування прийняття готівки. Натисніть клавішу 'M' після внесення готівки.",
                    "Поповнення", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Введіть коректну суму для поповнення.");
            }
        }

        private void txtCashAmount_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCashAmount_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void AutomatedTellerMachineForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.M && awaitingCash)
            {
                account.Balance += pendingDepositAmount;

                var repo = new AccountRepository();
                if (repo.UpdateAccount(account))
                {
                    MessageBox.Show("Кошти успішно зараховано. Новий баланс: " + account.Balance.ToString("0.00"),
                        "Успішно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCashAmount.Clear();
                }
                else
                {
                    MessageBox.Show("Помилка при оновленні балансу.");
                }

                awaitingCash = false;
                pendingDepositAmount = 0.0;
            }
        }
    }
}
