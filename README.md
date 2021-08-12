# CSharp(C#) Design Patterns
## Abstract Factory Design Pattern
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
## Adapter Design Pattern
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
## Builder Design Pattern
- Bir inşaatçı görev üstlenen yaklaşım sergilemektedir. Projemiz inşa süresindeyken oluşturacağımız bazı nesnelerin üretimleri oldukça maliyetli olabilir, zamanla bu nesnelerin yapısı değişebilir yahut güncellenebilir. Anlayacağınız nesne üzerinde her türlü dinamik süreç yaşanabilir. İşte bu tarz inşa durumlarında Builder Design Pattern ile ilgili nesneler genişletilebilir bir hale getirilmekte ve en önemlisi kod karmaşalığı minimize edilmektedir.
```csharp
using System;

namespace Builder
{
    class Programs
    {
        static void Main(string[] args)
        {
            ProductDirector productDirector = new ProductDirector();
            var builder = new NewCustomerProductBuilder();
            productDirector.GenerateProduct(builder);
            var model = builder.GetModel();
            Console.WriteLine("Category: "+model.CategoryName + "\n-----");
            Console.WriteLine("ProductName: " + model.ProductName + "\n-----");
            Console.WriteLine("Discount: " + model.Discount + "\n-----");
            Console.WriteLine("DiscountApplied: " + model.DiscountApplied + "\n-----");
            Console.WriteLine("Id: " + model.Id + "\n-----");
            Console.WriteLine("UnitPrice: " + model.UnitPrice + "\n-----");

            Console.ReadKey();
        }
    }

    public class ProductViewModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Discount { get; set; }
        public bool DiscountApplied { get; set; }
    }

    public abstract class ProductBuilder
    {
        public abstract void GetAll();
        public abstract void ApplyDiscount();
        public abstract ProductViewModel GetModel();
    }

    public class NewCustomerProductBuilder : ProductBuilder
    {
        private ProductViewModel model = new ProductViewModel();

        public override void GetAll()
        {
            model.Id = 1;
            model.CategoryName = "Beverages";
            model.ProductName = "Chai";
            model.UnitPrice = 20;
        }

        public override void ApplyDiscount()
        {
            model.Discount = 17;
            model.DiscountApplied = true;
        }

        public override ProductViewModel GetModel()
        {
            return model;
        }
    }

    public class OldCustomerProductBuilder : ProductBuilder
    {
        private ProductViewModel model = new ProductViewModel();

        public override void GetAll()
        {
            model.Id = 1;
            model.CategoryName = "Beverages";
            model.ProductName = "Chai";
            model.UnitPrice = 20;
        }

        public override void ApplyDiscount()
        {
            model.Discount = 17;
            model.DiscountApplied = false;
        }

        public override ProductViewModel GetModel()
        {
            return model;
        }
    }

    public class ProductDirector
    {
        public void GenerateProduct(ProductBuilder productBuilder)
        {
            productBuilder.GetAll();
            productBuilder.ApplyDiscount();
        }
    }
}
```
- [Kaynak 1](https://www.gencayyildiz.com/blog/c-builder-design-patternbuilder-tasarim-deseni/) 
- [Kaynak 2](https://tugrulbayrak.medium.com/builder-pattern-2f6fb1dbf4a0) 
## Facade Design Pattern
- Class kullanımını seviyelendiren bir tasarım desenidir. Basitçe herhangi bir class/fonksiyon içinden diğer class/fonksiyon'ları çağırmaya yarayan desendir. Facade deseni sistem karmaşıklığını gizler ve client(istemci)'nin sisteme erişmesini sağlayan bir arabirim görevi üstlenir. Bu desen bir grup class'ı ve özelliklerini (fonksiyon/değişken vb.) içeren tek bir ana sınıf olarak görev yapar.
```csharp
using System;

namespace Facade
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomerManager customer = new CustomerManager();
            customer.Save();

            Console.ReadKey();
        }
    }

    class Logging : ILogging
    {
        public void Log()
        {
            Console.WriteLine("Logged");
        }
    }

    internal interface ILogging
    {
        void Log();
    }

    class Caching : ICaching
    {
        public void Cache()
        {
            Console.WriteLine("Cached");
        }
    }

    internal interface ICaching
    {
        void Cache();
    }
    class Validate : IValidate
    {
        public void ValidateUser()
        {
            Console.WriteLine("Validate");
        }
    }

    internal interface IValidate
    {
        void ValidateUser();
    }
    class Authorize : IAuthorize
    {
        public void CheckUser()
        {
            Console.WriteLine("Checked");
        }
    }

    internal interface IAuthorize
    {
        void CheckUser();
    }

    class CustomerManager
    {
        private readonly CrossCuttingConcernsFacade _concerns;

        public CustomerManager()
        {
            _concerns = new CrossCuttingConcernsFacade();
        }

        public void Save()
        {
            _concerns.Logging.Log();
            _concerns.Authorize.CheckUser();
            _concerns.Validate.ValidateUser();
            _concerns.Caching.Cache();
            Console.WriteLine("Saved");
        }
    }

    class CrossCuttingConcernsFacade
    {
        public ILogging Logging;
        public ICaching Caching;
        public IAuthorize Authorize;
        public IValidate Validate;

        public CrossCuttingConcernsFacade()
        {
            Logging = new Logging();
            Caching = new Caching();
            Authorize = new Authorize();
            Validate = new Validate();
        }
    }
}
```
- [Kaynak 1](https://www.kadir.xyz/yazi/57/facade-tasarim-deseni-facade-pattern) 
- [Kaynak 2](https://www.gencayyildiz.com/blog/c-facade-design-patternfacade-tasarim-deseni/) 
## Factory Method Design Pattern
- Oluşturduğumuz bir interface ya da abstract sınıftan türeterek başka bir sınıf oluşturma işlemine verilen addır Factory Pattern. Oldukça sık kullanılan bu pattern aynı özelliği kullanmak isteyen sınıflar tarafından kullanılır.
```csharp
using System;

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
```
- [Kaynak 1](http://www.canertosuner.com/post/factory-method-pattern-nedir-nasil-kullanilir) 
- [Kaynak 2](https://blog.burakkutbay.com/design-pattern-factory-pattern-nedir.html/) 
