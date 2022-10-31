using System;
using System.Runtime.Serialization;

namespace AdvancedCSharp
{
    public class FileSystemVisitorException : Exception
    {
        public FileSystemVisitorException() : base()
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
