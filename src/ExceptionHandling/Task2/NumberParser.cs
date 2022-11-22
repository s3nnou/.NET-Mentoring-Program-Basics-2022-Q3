using System;
using System.Text.RegularExpressions;

namespace Task2
{
    public class NumberParser : INumberParser
    {
        public int Parse(string stringValue)
        {
            if (stringValue == null)
            {
                throw new ArgumentNullException();
            }
            stringValue = stringValue.Trim();

            ValidateString(stringValue);

            if (stringValue == int.MinValue.ToString())
            {
                return int.MinValue;
            }

            if (stringValue == int.MaxValue.ToString())
            {
                return int.MaxValue;
            }

            var isNegative = false;

            if (stringValue[0] == '-')
            {
                isNegative = true;
            }

            var y = 0;

            checked
            {
                for (int i = 0; i < stringValue.Length; i++)
                {
                    if (stringValue[i] == '-' || stringValue[i] == '+')
                    {
                        continue;
                    }

                    y = y * 10 + (stringValue[i] - '0');
                }
                if (isNegative) { y *= -1; }
            }

            return y;
        }

        private void ValidateString(string stringValue)
        {
            var regex = new Regex("^[+\\d|\\-d]?\\d+?$");
            if (!regex.IsMatch(stringValue))
            {
                throw new FormatException();
            }
        }
    }
}