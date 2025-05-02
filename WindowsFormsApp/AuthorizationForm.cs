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
    public partial class AuthorizationForm : Form
    {
        private AccountRepository accountRepository;
        public AuthorizationForm()
        {
            InitializeComponent();
            accountRepository = new AccountRepository();
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

        private void btnAuthorize_Click(object sender, EventArgs e)
        {
            string cardNumber = txtCardNumber.Text;
            string cardPIN = txtCardPIN.Text;

            Account account = accountRepository.GetAccount(cardNumber, cardPIN);

            if (account != null)
            {
                account.Authorizated += OnAuthorizated;

                if (account.ValidateAuthorization(cardNumber, cardPIN))
                {
                    AutomatedTellerMachineForm atmForm = new AutomatedTellerMachineForm(account);
                    atmForm.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Користувача не знайдено. Невірний PIN або номер картки.");
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
