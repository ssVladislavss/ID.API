using ID.Core.Exceptions.Base;
using System.Runtime.Serialization;

namespace ID.Core.Users.Exceptions
{
    public class UserFindAccessException : BaseIDException
    {
        public UserFindAccessException()
        {
        }

        public UserFindAccessException(string message) : base(message)
        {
        }

        public UserFindAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public UserFindAccessException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
