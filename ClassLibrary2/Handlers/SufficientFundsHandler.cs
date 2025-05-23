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

