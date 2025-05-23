using System.Collections.Generic;

namespace ClassLibrary2
{
    public class Bank
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public List<AutomatedTellerMachine> AutomatedTellerMachines { get; set; }

        public Bank(string name, string address)
        {
            Name = name;
            Address = address;
            AutomatedTellerMachines = new List<AutomatedTellerMachine>();
        }

        public void AddATM(AutomatedTellerMachine atm)
        {
            AutomatedTellerMachines.Add(atm);
        }
    }
}

