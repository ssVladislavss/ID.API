using System.Runtime.Serialization;

namespace ID.Core.Exceptions.Base
{
    public abstract class BaseIDException : Exception
    {
        public BaseIDException() { }
        public BaseIDException(string message) : base(message) { }
        protected BaseIDException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        protected BaseIDException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
