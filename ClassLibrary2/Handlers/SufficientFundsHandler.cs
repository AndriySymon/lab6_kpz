using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassLibrary2.Handlers
{
    public class SufficientFundsHandler : TransferHandler
    {
        public override HandlerResult Handle(Account sender, Account receiver, double amount)
        {
            if (sender.Balance < amount)
            {
                return HandlerResult.Fail("Недостатньо коштів.");
            }
            return base.Handle(sender, receiver, amount);
        }
    }
}

