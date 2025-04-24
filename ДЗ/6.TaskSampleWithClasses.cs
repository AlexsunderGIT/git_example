using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using static System.Net.WebRequestMethods;

namespace CSLight
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            List<Person> persons = new List<Person>()
             {
                new Person { FirstName = "Виталий", LastName = "Цаль", Age = 33 },
                new Person { FirstName = "Куджо", LastName = "Джотаро", Age = 40 },
                new Person { FirstName = "Юрий", LastName = "Хованский", Age = 34 },
                new Person { FirstName = "Михаил", LastName = "Петров", Age = 15 },
                new Person { FirstName = "Виталий", LastName = "Гачиев", Age = 40 },
                new Person { FirstName = "Юрий", LastName = "Гагарин", Age = 34 }
             };
            var countYura = persons.Count(player => player.FirstName == ("Юрий"));
            var FirstLastName = persons.Select(player => $"{player.FirstName} {player.LastName}").ToList();
            var AverageAge = persons.Average(player => player.Age);
            Console.WriteLine($"Сколько юр{countYura}");
            Console.WriteLine($"Имя фамилия {string.Join("\n", FirstLastName)}");
            Console.WriteLine($"Средний возраст{AverageAge}\n");
            var groupAge = persons.GroupBy(player => player.Age);
            foreach (var group in groupAge)
            {
                Console.WriteLine(group.Key);
                foreach (var person in group)
                {
                    Console.WriteLine(person.FirstName);
                }
                Console.WriteLine(); // для разделения между группами
            }
        }
        public class Person
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int Age { get; set; }
        }
    }
}
