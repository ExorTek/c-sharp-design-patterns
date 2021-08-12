using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomerManager customerManager = new CustomerManager();
            customerManager.Save();

            Console.ReadKey();
        }
    }

    class LoggerFactory : ILoggerFactory
    {
        public ILogger CreateLogger()
        {
            return new MdLogger();
        }
    }

    internal interface ILoggerFactory
    {
    }

    internal interface ILogger
    {
        void Log();
    }

    class MdLogger : ILogger
    {
        public void Log()
        {
            Console.WriteLine("Logged Md");

        }
    }

    class CustomerManager
    {
       public void Save()
        {
            Console.WriteLine("Saved!!");
            ILogger logger = new LoggerFactory().CreateLogger();
            logger.Log();
        }
    }
}