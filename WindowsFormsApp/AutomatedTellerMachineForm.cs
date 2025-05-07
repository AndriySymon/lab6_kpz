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
using ClassLibrary2.Handlers;
using ClassLibrary2.Interfaces;

namespace WindowsFormsApp
{
    public partial class AutomatedTellerMachineForm : Form
    {
        private Account account;
        private Account[] accounts;
        private AccountRepository accountRepository;
        private Account currentAccount;

        private readonly AccountRepository _repository;

        private double pendingDepositAmount = 0.0;
        private bool awaitingCash = false;

        private Timer sessionTimer;
        private TimeSpan sessionTimeout = TimeSpan.FromSeconds(30);
        private Form _loginForm;
        public AutomatedTellerMachineForm( Account account, AccountRepository repository)
        {
            InitializeComponent();
            currentAccount = account;
            InitializeBlinkingLabel();

            this.account = account;
            this._repository = repository;

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

            sessionTimer = new Timer();
            sessionTimer.Interval = 1000;
            sessionTimer.Tick += SessionTimer_Tick;
            sessionTimer.Start();
        }

        private int secondsInactive = 0;

        private void SessionTimer_Tick(object sender, EventArgs e)
        {
            secondsInactive++;

            if (secondsInactive >= sessionTimeout.TotalSeconds)
            {
                sessionTimer.Stop();
                MessageBox.Show("Сесію завершено через неактивність.");

                var authForm = new AuthorizationForm();

                authForm.Controls["txtName"].Text = "";
                authForm.Controls["txtCardNumber"].Text = "";
                authForm.Controls["txtCardPIN"].Text = "";
                authForm.Show();
                this.Close();
            }
        }
        private void ResetInactivityTimer()
        {
            secondsInactive = 0;
        }

        private void btnCheckBalance_Click(object sender, EventArgs e)
        {
            ResetInactivityTimer();
            account.CheckBalance();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            ResetInactivityTimer();
            this.Close();
            AuthorizationForm loginForm = new AuthorizationForm();
            loginForm.StartPosition = FormStartPosition.CenterScreen;
            loginForm.Show();
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            ResetInactivityTimer();
            string targetCardNumber = txtTransferCardNumber.Text.Trim();

            if (!double.TryParse(txtTransferAmount.Text, out double transferAmount) || transferAmount <= 0)
            {
                MessageBox.Show("Введіть коректну суму більше 0.");
                return;
            }

            IReadableAccount readableRepo = new AccountRepository();
            IUpdatableAccount updatableRepo = new AccountRepository();

            Account targetAccount = readableRepo.GetAccountByCardNumber(targetCardNumber);

            var handler1 = new ReceiverExistsHandler();
            var handler2 = new SufficientFundsHandler();
            var handler3 = new TransferLimitHandler();

            handler1.SetNext(handler2);
            handler2.SetNext(handler3);

            if (handler1.Handle(account, targetAccount, transferAmount))
            {
                account.Balance -= transferAmount;
                targetAccount.Balance += transferAmount;

                bool senderUpdated = updatableRepo.UpdateAccount(account);
                bool receiverUpdated = updatableRepo.UpdateAccount(targetAccount);

                if (senderUpdated && receiverUpdated)
                {
                    _repository.AddTransaction(new Transaction
                    {
                        CardNumber = account.CardNumber,
                        Type = "TransferOut",
                        Amount = transferAmount,
                        Timestamp = DateTime.Now
                    });

                    _repository.AddTransaction(new Transaction
                    {
                        CardNumber = targetAccount.CardNumber,
                        Type = "TransferIn",
                        Amount = transferAmount,
                        Timestamp = DateTime.Now
                    });

                    MessageBox.Show("Переказ успішно виконано!");
                    txtTransferAmount.Clear();
                    txtTransferCardNumber.Clear();
                }
                else
                {
                    MessageBox.Show("Помилка при оновленні акаунтів.");
                }
            }
        }


        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            ResetInactivityTimer();
            if (double.TryParse(txtWithdrawAmount.Text, out double withdrawAmount))
            {
                var validator = new AmountValidator();
                if (validator.IsValid(withdrawAmount, account.Balance, out string errorMessage))
                {
                    account.Balance -= withdrawAmount;

                    AccountRepository repo = new AccountRepository();
                    bool updated = repo.UpdateAccount(account);

                    if (updated)
                    {
                        var transaction = new Transaction
                        {
                            CardNumber = account.CardNumber,
                            Type = "Withdraw",
                            Amount = withdrawAmount,
                            Timestamp = DateTime.Now
                        };
                        repo.AddTransaction(transaction);

                        MessageBox.Show($"Готівку знято. Новий баланс: {account.Balance:0.00}");
                        txtWithdrawAmount.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Помилка при оновленні балансу.");
                    }
                }
                else
                {
                    MessageBox.Show(errorMessage);
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
            ResetInactivityTimer();
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
            ResetInactivityTimer();
            if (double.TryParse(txtCashAmount.Text, out double amount) && amount > 0)
            {
                pendingDepositAmount = amount;
                awaitingCash = true;

                _repository.AddTransaction(new Transaction
                {
                    CardNumber = currentAccount.CardNumber,
                    Type = "Deposit",
                    Amount = amount,
                    Timestamp = DateTime.Now
                });

                MessageBox.Show("Очікування прийняття готівки.",
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

        private void btnViewHistory_Click(object sender, EventArgs e)
        {
            ResetInactivityTimer();
            var historyForm = new TransactionHistoryForm(currentAccount, accountRepository);
            historyForm.ShowDialog();
        }
    }
}
