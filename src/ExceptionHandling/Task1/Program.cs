using System;

namespace Task1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Type text to see it first character:");

            while (true)
            {
                try
                {
                    var line = Console.ReadLine();
                    PrintFirstChar(line);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Oops. Error with the following text happend: {ex.Message}");
                }
            }
        }

        private static void PrintFirstChar(string line)
        {
            if (string.IsNullOrEmpty(line))
            {
                throw new ArgumentException("Entered line is null or empty");
            }

            Console.WriteLine($"Your first character is {line[0]}");
        }
    }
}