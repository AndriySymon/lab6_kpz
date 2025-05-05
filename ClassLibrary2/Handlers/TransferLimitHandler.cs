using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassLibrary2
{
    public class TransferLimitHandler : TransferHandler
    {
        private const double Limit = 5000;

        public override bool Handle(Account sender, Account receiver, double amount)
        {
            if (amount > Limit)
            {
                MessageBox.Show("Сума перевищує ліміт на один переказ.");
                return false;
            }

            return next?.Handle(sender, receiver, amount) ?? true;
        }
    }
}
