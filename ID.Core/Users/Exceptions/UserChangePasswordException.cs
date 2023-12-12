using ID.Core.Exceptions.Base;
using System.Runtime.Serialization;

namespace ID.Core.Users.Exceptions
{
    public class UserChangePasswordException : BaseIDException
    {
        public UserChangePasswordException()
        {
        }

        public UserChangePasswordException(string message) : base(message)
        {
        }

        public UserChangePasswordException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public UserChangePasswordException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
