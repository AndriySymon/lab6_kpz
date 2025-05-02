using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
