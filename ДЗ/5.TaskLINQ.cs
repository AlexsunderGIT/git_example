using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cslight.ДЗ
{
    internal class _5
    {
        public static void Main(string[] args)
        {
        List<string> names = new List<string>()
        {
            "Миша",
            "Вася",
            "Петя",
            "Гриша"
        };
        var firstName = names.First();
        var secAndThrdName = names.Skip(1).Take(2).ToList();
        var choiseFistLetterMName = names.FirstOrDefault(n => n.ToUpper().StartsWith("М"));
        var choiseLetterYaName = names.Where(n => n.Contains("я")).ToList();
        var countParagraphFour = choiseLetterYaName.Count;
        var HasPetya = names.Contains(("Петя"));
        var sortAlphabet = names.OrderBy(n => n).ToList();
        Console.WriteLine($"1){firstName}\n2){string.Join(",", secAndThrdName)}\n3){choiseFistLetterMName}\n4){string.Join(",", choiseLetterYaName)}");
        Console.WriteLine();
        Console.WriteLine(countParagraphFour);
        Console.WriteLine();
        Console.WriteLine($"Есть ли имя Петя. Ответ: {HasPetya}");
        Console.WriteLine();
        Console.WriteLine(string.Join(",", sortAlphabet));
        }
    }
}
