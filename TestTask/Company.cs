using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    internal class Company
    {
        public string Address { get; private set; }
        public string WorkingTime { get; private set; }
        public List<string> Phones { get; private set; }

        public Company(string address, string workingTime, List<string> phones) 
        {
            Address = address;
            WorkingTime = workingTime;
            Phones = phones;
        }

        public void GetInfo()
        {
            Console.WriteLine($"Address: {Address}");
            Console.WriteLine($"WorkingTime: {WorkingTime}");

            if( Phones.Count > 1 )
            {
                Console.WriteLine("Phones:");
                foreach( var phone in Phones )
                {
                    Console.WriteLine(phone);
                }
            }

            else
            {
                Console.WriteLine($"Phone: {Phones.FirstOrDefault()}");
            }
        }
    }
}
