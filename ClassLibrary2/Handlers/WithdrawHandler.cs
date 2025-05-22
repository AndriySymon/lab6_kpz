using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2.Handlers
{
    public abstract class WithdrawHandler
    {
        protected WithdrawHandler next;

        public void SetNext(WithdrawHandler nextHandler)
        {
            next = nextHandler;
        }

        public virtual HandlerResult Handle(Account account, double amount)
        {
            return next?.Handle(account, amount) ?? HandlerResult.Ok();
        }
    }
}

