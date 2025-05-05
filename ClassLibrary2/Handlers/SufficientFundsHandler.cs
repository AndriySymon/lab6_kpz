using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassLibrary2
{
    public class SufficientFundsHandler : TransferHandler
    {
        public override bool Handle(Account sender, Account receiver, double amount)
        {
            if (sender.Balance < amount)
            {
                MessageBox.Show("Недостатньо коштів.");
                return false;
            }

            return next?.Handle(sender, receiver, amount) ?? true;
        }
    }
}
