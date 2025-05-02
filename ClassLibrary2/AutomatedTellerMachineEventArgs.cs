using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2
{
    public class AutomatedTellerMachineEventArgs
    {
        public string Message {get;}

        public AutomatedTellerMachineEventArgs(string message)
        {
            Message = message;
        }
    }
}
