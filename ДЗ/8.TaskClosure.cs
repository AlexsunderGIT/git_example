using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cslight
{
    internal class Class3
    {
        public static void Main(string[] args)
        {
            var list = new List<int>();
            var i = 10;
            var query = list.Where(x => x == i).Where(x => x < 20);
            list.Add(10);
            list.Add(15);
            list.Add(20);
            i = 15;
            var result = query.ToList();
            list.Clear();
            Console.WriteLine(result.Count); // 1
            Console.WriteLine(result.FirstOrDefault()); // 15

            var funcs = new List<Func<int, int>>();
            for (int j = 0; j < 10; j++)
            {
                int save = j;//сохраняем значение i в другой переменной, потому что i замыкается в foreach при i =10
                funcs.Add(x => x + save);
            }
            foreach (var func in funcs)
            {
                Console.WriteLine(func(1)); // 11 Было стало 12345678910

            }

        }
    }
}