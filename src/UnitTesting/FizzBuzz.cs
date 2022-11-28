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
            throw new NotImplementedException();
        }

        public string PrintNumber(int number)
        {
            throw new NotImplementedException();
        }
    }
}
