using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassLibrary2.Handlers
{
    public class PositiveAmountHandler : WithdrawHandler
    {
        public override HandlerResult Handle(Account account, double amount)
        {
            if (amount <= 0)
            {
                return HandlerResult.Fail("Сума повинна бути більшою за 0.");
            }
            return base.Handle(account, amount);
        }
    }
}

