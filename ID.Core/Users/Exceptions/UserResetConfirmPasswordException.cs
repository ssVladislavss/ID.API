using ID.Core.Exceptions.Base;
using System.Runtime.Serialization;

namespace ID.Core.Users.Exceptions
{
    public class UserResetConfirmPasswordException : BaseIDException
    {
        public UserResetConfirmPasswordException()
        {
        }

        public UserResetConfirmPasswordException(string message) : base(message)
        {
        }

        public UserResetConfirmPasswordException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public UserResetConfirmPasswordException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
