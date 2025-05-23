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

