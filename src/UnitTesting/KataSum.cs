using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UnitTesting
{
    public interface IKataSum
    {
        public int Sum(string num1, string num2);

    }
    public class KataSum : IKataSum
    {
        public int Sum(string num1, string num2)
        {
            throw new NotImplementedException();
        }
    }
}
