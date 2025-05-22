using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassLibrary2.Handlers
{
    public class ReceiverExistsHandler : TransferHandler
    {
        public override HandlerResult Handle(Account sender, Account receiver, double amount)
        {
            if (receiver == null)
            {
                return HandlerResult.Fail("Карта призначення не знайдена.");
            }
            return base.Handle(sender, receiver, amount);
        }
    }
}

