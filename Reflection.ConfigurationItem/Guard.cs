using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.Models
{
    public static class Guard
    {
        private static readonly List<Type> ApprovedTypes = new() { typeof(int), typeof(float), typeof(string), typeof(TimeSpan) };

        public static bool IsAvailableForStorage(object source)
        {
            return IsAvailableForStorage(source.GetType());
        }

        public static bool IsAvailableForStorage(Type targetType)
        {
            return ApprovedTypes.Contains(targetType);
        }
    }
}
