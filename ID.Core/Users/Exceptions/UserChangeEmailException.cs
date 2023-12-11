using ID.Core.Exceptions.Base;
using System.Runtime.Serialization;

namespace ID.Core.Users.Exceptions
{
    public class UserChangeEmailException : BaseIDException
    {
        public UserChangeEmailException()
        {
        }

        public UserChangeEmailException(string message) : base(message)
        {
        }

        public UserChangeEmailException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public UserChangeEmailException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
