using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singeleton
{
    class Program
    {
        static void Main(string[] args)
        {
            var customer = CustomerManager.CreateAsSingleton();
            customer.Save();
            Console.ReadKey();
        }
    }

    class CustomerManager
    {
        private static CustomerManager _customerManager;

        private CustomerManager()
        {
        }

        public static CustomerManager CreateAsSingleton()
        {
            return _customerManager ?? (_customerManager = new CustomerManager());
        }

        public  void Save()
        {
            Console.WriteLine("Saved!!");
        }
    }
}