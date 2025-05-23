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

