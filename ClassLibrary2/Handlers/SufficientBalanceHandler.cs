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
        public override HandlerResult Handle(Account account, double amount)
        {
            if (amount > account.Balance)
            {
                return HandlerResult.Fail("На рахунку недостатньо коштів.");
            }
            return base.Handle(account, amount);
        }
    }
}

