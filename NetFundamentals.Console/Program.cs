using NetFundamentals.ClassLibrary;

namespace NetFundamentals.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length <= 0)
            {
                System.Console.WriteLine("No arguments provided");

                return;
            }

            var salutation = $"Hello, {args[0]}";

            System.Console.WriteLine(salutation);
            System.Console.WriteLine(salutation.AddTimeStamp());
        }
    }
}