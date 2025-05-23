using System;

namespace ClassLibrary2
{
    public class AutomatedTellerMachine
    {
        public delegate void AutomatedTellerMachineStateHandler( object sender, AutomatedTellerMachineEventArgs e);
        public event AutomatedTellerMachineStateHandler CheckedBalance;
        public int ID { get; set; }
        public string Address { get; set; }
        public AutomatedTellerMachine(string address) {
            ID = new Random().Next(0, 1000);
            Address = address;
        }
    }
}
