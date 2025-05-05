using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassLibrary2
{
    public class ReceiverExistsHandler : TransferHandler
    {
        public override bool Handle(Account sender, Account receiver, double amount)
        {
            if (receiver == null)
            {
                MessageBox.Show("Карта призначення не знайдена.");
                return false;
            }

            return next?.Handle(sender, receiver, amount) ?? true;
        }
    }
}
