using System;

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
