using ID.Core.Exceptions.Base;
using System.Runtime.Serialization;

namespace ID.Core.Users.Exceptions
{
    public class UserChangePhoneNumberException : BaseIDException
    {
        public UserChangePhoneNumberException()
        {
        }

        public UserChangePhoneNumberException(string message) : base(message)
        {
        }

        public UserChangePhoneNumberException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public UserChangePhoneNumberException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
