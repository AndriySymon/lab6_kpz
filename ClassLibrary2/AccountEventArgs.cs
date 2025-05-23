using System;

namespace ClassLibrary2
{
    public class AccountEventArgs : EventArgs
    {
        public string Message {  get;}
        public AccountEventArgs(string message)
        {
            Message = message;
        }
    }
}
