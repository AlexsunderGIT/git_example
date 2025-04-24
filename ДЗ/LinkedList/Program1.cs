using System;
using System.Collections.Generic;
using Cslight.Model;


namespace Cslight
{
     class Program1
     {
        static void Main(string[] args)
        {
            var list = new Model.LinkedList<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(5);
            list.Add(6);
            list.Add(7);
            list.Add(8);
            list.Add(9);
            list.Add(10);
            list.InsertAfter(5,42);
            list.Replace(9,-1);
            list.Delete(3);

            Console.WriteLine($"длина списка: {list.Count} элементов");
            Console.WriteLine($"хапи-хапи-хапи\nПИЗДАТО");

            foreach (var item in list)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
        }
     }
}
