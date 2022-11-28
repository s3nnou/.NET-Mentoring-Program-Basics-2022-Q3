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
        public string PrintAll()
        {
            var stringBuilder = new StringBuilder();

            for (var i = 1; i <= 100; i++)
            {
                stringBuilder.AppendLine(PrintNumber(i));
            }

            return stringBuilder.ToString();
        }

        public string PrintNumber(int number)
        {
            if (number <= 0 || number > 100)
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

            var fizzBuzzString = fizzBuzzBuilder.ToString();
            return string.IsNullOrEmpty(fizzBuzzString) ? number.ToString() : fizzBuzzString;
        }

        private static bool IsDivededByThree(int number) => number % 3 == 0;

        private static bool IsDivededByFive(int number) => number % 5 == 0;
    }
}
