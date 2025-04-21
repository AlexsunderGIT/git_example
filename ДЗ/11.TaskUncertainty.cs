using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Cslight
{
    internal class Class5
    {
        public static void Main(string[] args)
        {
            Dictionary<B, string> map = new Dictionary<B, string>();
            var el1 = new B();
            var el2 = new B();
            map[el1] = "12";
            map[el2] = "14";

            Console.WriteLine(map[el1]);
            Console.WriteLine(map[el2]);


        }
        class B
        {
            private readonly Random _random = new Random();
            private readonly int _x = new Random().Next();
            //public override int GetHashCode() => _random.Next();
            public override int GetHashCode() => _x;

            public override bool Equals(object obj) => true;
            ////тут прекол в дебаге всё окей(12,14), а при обычном компилировании, компилятор даёт ёбу и ебашит (14,14) Андрюха говорил компилятор пидрила

        }

    }

}
