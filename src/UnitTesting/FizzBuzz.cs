using System.Text;

namespace UnitTesting
{
    public interface IFizzBuzz
    {
        string PrintNumber(int number);

        string PrintAll();
    }

    public class FizzBuzz : IFizzBuzz
    {
        private const int Count = 100;

        public string PrintAll()
        {
            var stringBuilder = new StringBuilder();

            for (var i = 1; i <= Count; i++)
            {
                stringBuilder.AppendLine(PrintNumber(i));
            }

            return stringBuilder.ToString();
        }

        public string PrintNumber(int number)
        {
            if (number <= 0 || number > Count)
                throw new ArgumentException();

            return DetermineFizzOrBuzz(number);
        }

        private static string DetermineFizzOrBuzz(int number)
        {
            var fizzBuzzBuilder = new StringBuilder();

            if (IsDivededByThree(number))
            {
                fizzBuzzBuilder.Append("Fizz");
            }

            if (IsDivededByFive(number))
            {
                fizzBuzzBuilder.Append("Buzz");
            }

            return fizzBuzzBuilder.Length == 0 ? number.ToString() : fizzBuzzBuilder.ToString();
        }

        private static bool IsDivededByThree(int number) => number % 3 == 0;

        private static bool IsDivededByFive(int number) => number % 5 == 0;
    }
}