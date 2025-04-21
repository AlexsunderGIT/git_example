using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cslight
{
    internal class Class4
    {
    }
    namespace Cslight.Properties
    {
        internal class Class1
        {
            public static void Main(string[] args)
            {
            }

            List<Person> persons = new List<Person>()
            {
            new Person { FirstName = "Виталий", LastName = "Цаль", Age = 33 },
            new Person { FirstName = "Куджо", LastName = "Джотаро", Age = 40 },
            new Person { FirstName = "Юрий", LastName = "Хованский", Age = 34 },
            new Person { FirstName = "Михаил", LastName = "Петров", Age = 15 },
            new Person { FirstName = "Виталий", LastName = "Гачиев", Age = 40 },
            new Person { FirstName = "Юрий", LastName = "Гагарин", Age = 34 },
            };
            List<Person> FilterPersons(
            List<Person> all,
            string firstName = null,
            string lastName = null,
            int? age = null)

            {
                IEnumerable<Person> query = all;
                if (firstName != null)
                {
                    query = query.Where(x => x.FirstName == firstName);
                }
                if (age != null)
                {
                    query = query.Where(x => x.Age == age);
                }
                if (lastName != null)
                {
                    query = query.Where(x => x.LastName == lastName);
                }

                return query.ToList();
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
