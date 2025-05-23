using System;

namespace ClassLibrary2
{
    public class AccountBalanceEventArgs : EventArgs
    {
        public string Message { get; }
        public double Sum {  get; }
        public AccountBalanceEventArgs(string message, double sum)
        {
            Message = message;
            Sum = sum;
        }
    }
}
