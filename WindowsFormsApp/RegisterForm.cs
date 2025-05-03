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
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }
        public string GenerateCardNumber()
        {
            Random rnd = new Random();
            StringBuilder cardNumber = new StringBuilder("4149");

            for (int i = 0; i < 12; i++)
            {
                cardNumber.Append(rnd.Next(0, 10));
            }

            return cardNumber.ToString();
        }

        private string generatedCardNumber;

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            generatedCardNumber = GenerateCardNumber();
        }

        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            var repo = new AccountRepository();

            var account = new Account(
            generatedCardNumber,
            txtCardPIN.Text,
            txtFirstName.Text,
            txtLastName.Text,
            txtEmail.Text,
            txtPhone.Text,
            0.0,
            0.0
            );
            if (repo.AddAccount(account))
            {
                MessageBox.Show(
                    "Акаунт успішно створено! Ваш згенерований номер картки:\n" + generatedCardNumber +
                    "\n\nЩоб отримати фізичну картку, зверніться у найближче відділення банку." +
                    "\n\nКартка автоматично з'явиться у вашому додатку Приватбанку. Дякую, що обрали нас!",
                    "Номер картки",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                this.Hide();
                AuthorizationForm loginForm = new AuthorizationForm();
                loginForm.StartPosition = FormStartPosition.CenterScreen;
                loginForm.Show();
            }
            else
            {
                MessageBox.Show("Помилка при створенні акаунту");
            }
        }

        private void btnBackToLogin_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
