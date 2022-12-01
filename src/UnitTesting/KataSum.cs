using System.Text.RegularExpressions;

namespace UnitTesting
{
    public interface IKataSum
    {
        int Sum(string num1, string num2);
    }

    public class KataSum : IKataSum
    {
        public int Sum(string num1, string num2)
        {
            var parsedNum1 = Parse(num1);
            var parsedNum2 = Parse(num2);

            return parsedNum1 + parsedNum2;
        }

        private int Parse(string stringValue)
        {
            if (string.IsNullOrEmpty(stringValue))
            {
                return 0;
            }

            stringValue = stringValue.Trim();

            if (!IsNaturalNumber(stringValue))
            {
                return 0;
            }

            if (stringValue == int.MaxValue.ToString())
            {
                return int.MaxValue;
            }

            var y = 0;

            checked
            {
                for (int i = 0; i < stringValue.Length; i++)
                {
                    y = y * 10 + (stringValue[i] - '0');
                }

            }

            return y;
        }

        private bool IsNaturalNumber(string stringValue)
        {
            var regex = new Regex("^[1-9][0-9]*$");
            return regex.IsMatch(stringValue);
        }
    }
}
