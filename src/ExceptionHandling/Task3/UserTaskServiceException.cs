using System;
using System.Runtime.Serialization;

namespace Task3
{
    public class UserTaskServiceException : Exception
    {
        public UserTaskServiceException() : base()
        {
        }

        public UserTaskServiceException(string message) : base(message)
        {
        }

        public UserTaskServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserTaskServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
