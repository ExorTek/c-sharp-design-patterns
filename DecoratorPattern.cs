using System;

namespace Decorator
{
    class Program
    {
        static void Main(string[] args)
        {
            var commercialCar = new CommercialCar()
            {
                HirePrice = 500,
                Brand = "BMW",
                Model = "8 Series"
            };

            var personalCar = new PersonalCar
            {
                Brand = "Mercedes",
                HirePrice = 200,
                Model = "Amg"
            };
            SpecialOffer specialOffer = new SpecialOffer(personalCar)
            {
                DiscountPercentage = 10
            };
            SpecialOffer specialOffer2 = new SpecialOffer(commercialCar)
            {
                DiscountPercentage = 5
            };

            Console.WriteLine("Concrete : " + personalCar.HirePrice);
            Console.WriteLine("Special Offer : " + specialOffer.HirePrice);
            Console.WriteLine("-------------");
            Console.WriteLine("Concrete : " + commercialCar.HirePrice);
            Console.WriteLine("Special Offer : " + specialOffer2.HirePrice);

            Console.ReadKey();
        }
    }

    abstract class CarBase
    {
        public abstract string Brand { get; set; }
        public abstract string Model { get; set; }
        public abstract decimal HirePrice { get; set; }
    }

    class PersonalCar : CarBase
    {
        public override string Brand { get; set; }
        public override string Model { get; set; }
        public override decimal HirePrice { get; set; }
    }

    class CommercialCar : CarBase
    {
        public override string Brand { get; set; }
        public override string Model { get; set; }
        public override decimal HirePrice { get; set; }
    }

    abstract class CarDecoratorBase : CarBase
    {
        private CarBase _carBase;

        protected CarDecoratorBase(CarBase carBase)
        {
            _carBase = carBase;
        }
    }

    class SpecialOffer : CarDecoratorBase
    {
        public int DiscountPercentage { get; set; }
        private readonly CarBase _carBase;

        public SpecialOffer(CarBase carBase) : base(carBase)
        {
            _carBase = carBase;
        }

        public override string Brand { get; set; }
        public override string Model { get; set; }

        public override decimal HirePrice
        {
            get => _carBase.HirePrice -
                   _carBase.HirePrice * DiscountPercentage / 100; //alternative --> get { return _carBase.HirePrice;}
            set { }
        }
    }
}