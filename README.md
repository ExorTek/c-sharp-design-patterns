# CSharp(C#) Design Patterns
## Abstract Factory
- İlişkisel olan birden fazla nesnenin üretimini tek bir arayüz tarafından değil her ürün ailesi için farklı bir arayüz tanımlayarak sağlamaktadır.
```csharp
using System;

namespace AbstractFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductManager productManager = new ProductManager(new Factory1());
            productManager.GetAll();

            Console.ReadKey();
        }
    }

    public abstract class Logging
    {
        public abstract void Log(string message);
    }

    public class Log4NetLogger : Logging
    {
        public override void Log(string message)
        {
            Console.WriteLine("Log with log4net");
        }
    }

    public class NLogger : Logging
    {
        public override void Log(string message)
        {
            Console.WriteLine("Log with NLogger");
        }
    }

    public abstract class Caching
    {
        public abstract void Cache(string data);
    }

    public class MemCache : Caching
    {
        public override void Cache(string data)
        {
            Console.WriteLine("Cache with MemCache");
        }
    }

    public class RedisCache : Caching
    {
        public override void Cache(string data)
        {
            Console.WriteLine("Cache with RedisCache");
        }
    }

    public abstract class CrossCuttingConcernsFactory
    {
        public abstract Logging CreateLogger();
        public abstract Caching CreateCaching();
    }

    public class Factory1 : CrossCuttingConcernsFactory
    {
        public override Logging CreateLogger()
        {
            return new Log4NetLogger();
        }

        public override Caching CreateCaching()
        {
            return new RedisCache();
        }
    }
    public class Factory2 : CrossCuttingConcernsFactory
    {
        public override Logging CreateLogger()
        {
            return new NLogger();
        }

        public override Caching CreateCaching()
        {
            return new MemCache();
        }
    }

    public class ProductManager
    {
        private CrossCuttingConcernsFactory _crossCuttingConcernsFactory;

        private Logging _logging;
        private Caching _caching;
        public ProductManager(CrossCuttingConcernsFactory crossCuttingConcernsFactory)
        {
            _crossCuttingConcernsFactory = crossCuttingConcernsFactory;
            _logging = _crossCuttingConcernsFactory.CreateLogger();
            _caching = _crossCuttingConcernsFactory.CreateCaching();
        }

        public void GetAll()
        {
            _logging.Log("log");
            _caching.Cache("data");
            Console.WriteLine("Products Listed");
        }
    }
}
```
- [Kaynak 1](https://hakantopuz.medium.com/abstract-factory-design-pattern-nedir-ne-zaman-ve-nas%C4%B1l-kullan%C4%B1l%C4%B1r-25dea188477c) 
- [Kaynak 2](https://yasinmemic.medium.com/abstract-factory-design-pattern-d142de6a883c) 
## Adapter
- Sadece bir sınıfa özel olan arayüzleri diğer sınıflarla uyumlu arayüzler haline getiren bir tasarım kalıbıdır. Adaptörler uyumlu olmayan arayüzler sebebiyle birbirleri ile çalışamayan sınıflara da birbirleri ile çalışma imkanı sunarlar.
```csharp
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
```
- [Kaynak 1](https://metinalniacik.medium.com/adapter-design-pattern-tasar%C4%B1m-%C3%B6r%C3%BCnt%C3%BCs%C3%BC-3469833059d9) 
- [Kaynak 2](https://medium.com/kodcular/adapt%C3%B6rdesign-pattern-adaptor-tasar%C4%B1m-deseni-a68ee58a35c2) 
