using ID.Core.Exceptions.Base;
using System.Runtime.Serialization;

namespace ID.Core.Users.Exceptions
{
    public class UserEmailConfirmException : BaseIDException
    {
        public UserEmailConfirmException()
        {
        }

        public UserEmailConfirmException(string message) : base(message)
        {
        }

        public UserEmailConfirmException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public UserEmailConfirmException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
