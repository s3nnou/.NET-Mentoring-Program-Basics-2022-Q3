using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.Models
{
    public class ReflectionException : Exception
    {
        public ReflectionException() : base()
        {
        }

        public ReflectionException(string? message) : base(message)
        {
        }

        public ReflectionException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ReflectionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
