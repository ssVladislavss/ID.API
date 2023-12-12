using ID.Core.Exceptions.Base;
using System.Runtime.Serialization;

namespace ID.Core.Users.Exceptions
{
    public class UserConfirmPhoneNumberException : BaseIDException
    {
        public UserConfirmPhoneNumberException()
        {
        }

        public UserConfirmPhoneNumberException(string message) : base(message)
        {
        }

        public UserConfirmPhoneNumberException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public UserConfirmPhoneNumberException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
