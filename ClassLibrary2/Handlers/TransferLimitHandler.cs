using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassLibrary2.Handlers
{
    public class TransferLimitHandler : TransferHandler
    {
        private const double Limit = 5000;

        public override HandlerResult Handle(Account sender, Account receiver, double amount)
        {
            if (amount > Limit)
            {
                return HandlerResult.Fail("Сума перевищує ліміт на один переказ.");
            }

            return base.Handle(sender, receiver, amount);
        }
    }
}

