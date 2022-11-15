using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection
{
    public static class ReflectionExtensions
    {
        public static object TryConvertToPropertyType(this string value, Type propertyType)
        {
            if (typeof(string) == propertyType)
            {
                return value;
            }
            else if (typeof(int) == propertyType)
            {
                return int.Parse(value);
            }
            else if (typeof(float) == propertyType)
            {
                return float.Parse(value);
            }
            else if (typeof(TimeSpan) == propertyType)
            {
                return TimeSpan.Parse(value);
            }
            else
            {
                throw new ArgumentException("Provided property is not string, int, float or TimeSpan");
            }
        }
    }
}
