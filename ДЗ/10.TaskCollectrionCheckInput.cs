using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cslight
{
    internal class Class7
    {
        public static void Main(string[] args)
        {
            HashSet<A> @set = new HashSet<A>();
            var el1 = new A { Value = 1 };
            var el2 = new A { Value = 1 };
            @set.Add(el1);
            Console.WriteLine(@set.Contains(el2));

        }
        struct A
        {
            public int Value { get; set; }
            public override int GetHashCode()
            {
                Console.WriteLine("GetHashCode");
                return Value;
            }
        }
    }
}
