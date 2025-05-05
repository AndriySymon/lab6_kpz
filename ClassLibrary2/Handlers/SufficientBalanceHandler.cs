using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassLibrary2.Handlers
{
    public class SufficientBalanceHandler : WithdrawHandler
    {
        public override bool Handle(Account account, double amount)
        {
            if (amount > account.Balance)
            {
                MessageBox.Show("На рахунку недостатньо коштів.");
                return false;
            }
            return next?.Handle(account, amount) ?? true;
        }
    }

}
