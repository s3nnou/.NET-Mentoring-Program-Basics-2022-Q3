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
            if (IsDivededByThree(number) && IsDivededByFive(number))
            {
                return "FizzBuzz";
            }
            else if (IsDivededByFive(number))
            {
                return "Buzz";
            }
            else if (IsDivededByThree(number))
            {
                return "Fizz";
            }
            else
            {
                return number.ToString();
            }
        }

        private static bool IsDivededByThree(int number) => number % 3 == 0;

        private static bool IsDivededByFive(int number) => number % 5 == 0;
    }
}
