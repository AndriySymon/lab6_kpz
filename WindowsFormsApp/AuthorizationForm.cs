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
using ClassLibrary2.Interfaces;

namespace WindowsFormsApp
{
    public partial class AuthorizationForm : Form
    {
        private IReadableAccount readableRepository;
        private IUpdatableAccount updatableRepository;
        public AuthorizationForm()
        {
            InitializeComponent();
            var repo = new AccountRepository();
            readableRepository = repo;
            updatableRepository = repo;
        }
        private void AuthorizationForm_Load(object sender, EventArgs e)
        {
                
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void OnAuthorizated(object sender, AccountEventArgs e)
        {
            MessageBox.Show(e.Message);
        }

        private const int MaxLoginAttempts = 3;
        private readonly TimeSpan LockDuration = TimeSpan.FromMinutes(2);
        private void btnAuthorize_Click(object sender, EventArgs e)
        {
            string cardNumber = txtCardNumber.Text.Trim();
            string cardPIN = txtCardPIN.Text.Trim();

            Account account = readableRepository.GetAccountByCardNumber(cardNumber);

            if (account == null)
            {
                MessageBox.Show("Користувача не знайдено.");
                return;
            }

            if (account.IsLocked)
            {
                if (account.LockTime.HasValue && DateTime.Now >= account.LockTime.Value.Add(LockDuration))
                {
                    account.IsLocked = false;
                    account.FailedLoginAttempts = 0;
                    account.LockTime = null;
                    updatableRepository.UpdateAccount(account);
                }
                else
                {
                    TimeSpan timeLeft = account.LockTime.Value.Add(LockDuration) - DateTime.Now;
                    MessageBox.Show($"Акаунт заблокований. Спробуйте знову через {timeLeft.Minutes} хв {timeLeft.Seconds} сек.");
                    return;
                }
            }

            if (account.CardPIN == cardPIN)
            {
                account.FailedLoginAttempts = 0;
                updatableRepository.UpdateAccount(account);

                account.Authorizated += OnAuthorizated;
                if (account.ValidateAuthorization(cardNumber, cardPIN))
                {
                    var repo = new AccountRepository();
                    AutomatedTellerMachineForm atmForm = new AutomatedTellerMachineForm(account, repo);
                    atmForm.Show();
                    this.Hide();
                }
            }
            else
            {
                account.FailedLoginAttempts++;

                if (account.FailedLoginAttempts >= MaxLoginAttempts)
                {
                    account.IsLocked = true;
                    account.LockTime = DateTime.Now;
                    MessageBox.Show("Акаунт заблоковано через багато невдалих спроб входу.");
                }
                else
                {
                    MessageBox.Show($"Невірний PIN. Залишилось спроб: {MaxLoginAttempts - account.FailedLoginAttempts}");
                }

                updatableRepository.UpdateAccount(account);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            var registerForm = new RegisterForm();
            registerForm.StartPosition = FormStartPosition.CenterScreen;
            this.Hide();
            registerForm.ShowDialog();
            this.Show();
        }
    }
}
