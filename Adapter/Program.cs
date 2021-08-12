using System;

namespace Adapter
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductManager productManager = new ProductManager(new MdLogger());
            productManager.Save();
            Console.WriteLine("-----------------");
            ProductManager productManager1 = new ProductManager(new Log4NetAdapter());
            productManager1.Save();
            Console.ReadKey();
        }
    }

    class ProductManager : IProductService
    {
        private ILogger _logger;

        public ProductManager(ILogger logger)
        {
            _logger = logger;
        }

        public void Save()
        {
            _logger.Log("UserData");
            Console.WriteLine("Saved");
        }
    }

    interface IProductService
    {
        void Save();
    }

    interface ILogger
    {
        void Log(string message);
    }

    class MdLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"Logged with mdlogger, {message}");
        }
    }

    class Log4Net
    {
        public void LogMessage(string message)
        {
            Console.WriteLine($"Logged with log4net, {message}");
        }
    }

    class Log4NetAdapter : ILogger
    {
        public void Log(string message)
        {
            Log4Net log4Net = new Log4Net();
            log4Net.LogMessage(message);
        }
    }
}