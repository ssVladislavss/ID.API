using ID.Core.Exceptions.Base;
using System.Runtime.Serialization;

namespace ID.Core.Users.Exceptions
{
    public class UserAddAccessException : BaseIDException
    {
        public UserAddAccessException()
        {
        }

        public UserAddAccessException(string message) : base(message)
        {
        }

        public UserAddAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public UserAddAccessException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
