using System;
using System.Reflection;

namespace TsTyper
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputPath = Console.ReadLine();
            var outputPath = Console.ReadLine();
            var namespacePath = Console.ReadLine();
            try
            {
               var count = Parser.Parse(inputPath, "./", namespacePath);
               Console.WriteLine($"Successfully exported {count} type(s)!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Console.ReadLine();
            }

            Console.ReadLine();
        }
    }
}
