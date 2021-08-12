using System;

namespace Prototype
{
    class Program
    {
        static void Main(string[] args)
        {
            Customer customer1 = new Customer
            {
                City = "Los Angeles",
                Id = 1,
                Name = "Memet",
                Surname = "D."
            };
            Customer customer2 = (Customer)customer1.Clone();
            customer2.Name = "John";
            Console.WriteLine(customer1.Name+"\n");
            Console.WriteLine(customer2.Name);

            Console.ReadKey();
        }
    }

    public abstract class Person
    {
        public abstract Person Clone();
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }

    public class Customer : Person
    {
        public string City { get; set; }

        public override Person Clone()
        {
            return (Person)MemberwiseClone();
        }
    }

    public class Employee : Person
    {
        public decimal Salary { get; set; }

        public override Person Clone()
        {
            return (Person)MemberwiseClone();
        }
    }
}