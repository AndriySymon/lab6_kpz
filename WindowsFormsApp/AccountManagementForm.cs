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
    public partial class AccountManagementForm : Form
    {
        private Account currentAccount;

        private IDeletableAccount repo;
        private IUpdatableAccount repoUpdate;

        public AccountManagementForm(Account account)
        {
            InitializeComponent();
            currentAccount = account;
            var repository = new AccountRepository();
            repo = repository;
            repoUpdate = repository;
        }

        private void AccountManagementForm_Load(object sender, EventArgs e)
        {
            txtCardNumber.Text = currentAccount.CardNumber;
            txtFirstName.Text = currentAccount.FirstName;
            txtLastName.Text = currentAccount.LastName;
            txtPhone.Text = currentAccount.Phone;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            currentAccount.FirstName = txtFirstName.Text;
            currentAccount.LastName = txtLastName.Text;
            currentAccount.Phone = txtPhone.Text;

            bool updated = repoUpdate.UpdateAccount(currentAccount);

            if (updated)
                MessageBox.Show("Акаунт оновлено успішно!", "Оновлення", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Помилка при оновленні.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("Ви впевнені, що хочете видалити акаунт?", "Підтвердження",
                                  MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                bool deleted = repo.DeleteAccount(currentAccount.CardNumber);

                if (deleted)
                {
                    MessageBox.Show("Акаунт видалено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    var loginForm = new AuthorizationForm();
                    loginForm.StartPosition = FormStartPosition.CenterScreen;
                    loginForm.Show();
                }
                else
                {
                    MessageBox.Show("Помилка при видаленні.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
