using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2
{
    public abstract class TransferHandler
    {
        protected TransferHandler next;

        public void SetNext(TransferHandler nextHandler)
        {
            next = nextHandler;
        }

        public abstract bool Handle(Account sender, Account receiver, double amount);
    }
}
