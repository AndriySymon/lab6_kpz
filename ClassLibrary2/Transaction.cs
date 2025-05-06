using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2
{
    public class Transaction
    {
        public string CardNumber { get; set; }
        public string Type { get; set; }
        public double Amount { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
