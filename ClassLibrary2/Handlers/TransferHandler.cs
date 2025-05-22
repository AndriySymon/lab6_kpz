using ClassLibrary2.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2.Handlers
{
    public abstract class TransferHandler
    {
        protected TransferHandler next;

        public void SetNext(TransferHandler nextHandler)
        {
            next = nextHandler;
        }

        public virtual HandlerResult Handle(Account sender, Account receiver, double amount)
        {
            return next?.Handle(sender, receiver, amount) ?? HandlerResult.Ok();
        }
    }
}

