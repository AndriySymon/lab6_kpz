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

        public override HandlerResult Handle(Account account, double amount)
        {
            if (amount > MaxWithdrawLimit)
            {
                return HandlerResult.Fail($"Максимальна сума для зняття — {MaxWithdrawLimit}.");
            }

            return base.Handle(account, amount);
        }
    }
}

