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
    public partial class TransactionHistoryForm : Form
    {

        private readonly Account _account;
        private readonly AccountRepository _repository;
        public TransactionHistoryForm(Account account, AccountRepository accountRepository)
        {
            InitializeComponent();
            _account = account;
            _repository = accountRepository;
        }

        private void TransactionHistoryForm_Load(object sender, EventArgs e)
        {
            var history = _repository.GetTransactionHistory(_account.CardNumber);
            dataGridViewTransactions.DataSource = history.Select(h => new
            {
                Тип = h.Type,
                Сума = h.Amount,
                Дата = h.Timestamp
            }).ToList();
        }
    }
}
