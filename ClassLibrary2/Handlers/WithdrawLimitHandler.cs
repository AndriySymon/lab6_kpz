using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassLibrary2.Handlers
{
    public class WithdrawLimitHandler : WithdrawHandler
    {
        private const double MaxWithdrawLimit = 10000;

        public override bool Handle(Account account, double amount)
        {
            if (amount > MaxWithdrawLimit)
            {
                MessageBox.Show($"Максимальна сума для зняття — {MaxWithdrawLimit}.");
                return false;
            }
            return next?.Handle(account, amount) ?? true;
        }
    }
}
