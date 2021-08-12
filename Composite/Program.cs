using System;
using System.Collections;
using System.Collections.Generic;

namespace Composite
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee memet = new Employee()
            {
                Name = "Memet"
            };
            Employee joe = new Employee()
            {
                Name = "Joe"
            };
            Employee kate = new Employee()
            {
                Name = "Kate"
            };

            memet.AddSubordinate(memet);
            joe.AddSubordinate(joe);
            kate.AddSubordinate(kate);

            foreach (var executive in memet) 
            {
                Console.WriteLine($"-> Executive: {executive.Name}");
                foreach (var manager in joe)
                {
                    Console.WriteLine($"  -> Manager: {manager.Name}");
                    foreach (var employee in kate)
                    {
                        Console.WriteLine($"    -> Employee: {employee.Name}");
                    }
                }
            }

            Console.ReadKey();
        }
    }

    interface IPerson
    {
        string Name { get; set; }
    }

    class Employee : IPerson, IEnumerable<IPerson>
    {
        public readonly List<IPerson> Subordinates = new List<IPerson>();

        public void AddSubordinate(IPerson person)
        {
            Subordinates.Add(person);
        }

        public void RemoveSubordinate(IPerson person)
        {
            Subordinates.Remove(person);
        }

        public IPerson GetSubordinate(int index)
        {
            return Subordinates[index];
        }

        public string Name { get; set; }

        public IEnumerator<IPerson> GetEnumerator()
        {
            foreach (var subordinate in Subordinates)
            {
                yield return subordinate;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}