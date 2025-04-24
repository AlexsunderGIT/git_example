using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cslight.ДЗ
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            ConstantReader constantReader = new ConstantReader();
            ConsoleReader consoleReader = new ConsoleReader();
            InegerParser parser1 = new InegerParser(constantReader);
            InegerParser parser2 = new InegerParser(consoleReader);
            Console.WriteLine(parser1.Parse());
            Console.WriteLine("пу-пу-пу");
            Console.WriteLine(parser2.Parse());
        }
        interface IInputReader
        {
            string ReadFromSource();
        }
        class ConstantReader : IInputReader
        {
            public string ReadFromSource() => "5";
        }
        class ConsoleReader : IInputReader
        {
            public string ReadFromSource() => Console.ReadLine();
        }
        class InegerParser
        {
            private readonly IInputReader _ineger;
            public InegerParser(IInputReader Reader)
            {
                _ineger = Reader;
            }
            public int Parse()
            {
                var inputedValue = _ineger.ReadFromSource();

                return int.Parse(inputedValue);
            }
        }
    }
}

