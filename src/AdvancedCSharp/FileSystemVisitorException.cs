using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedCSharp
{
    public class FileSystemVisitorException : Exception
    {
        public FileSystemVisitorException()
        {
        }

        public FileSystemVisitorException(string? message) : base(message)
        {
        }

        public FileSystemVisitorException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected FileSystemVisitorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
