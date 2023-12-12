using ID.Core.Exceptions.Base;
using System.Runtime.Serialization;

namespace ID.Core.Users.Exceptions
{
    public class UserSetLockoutEnabledException : BaseIDException
    {
        public UserSetLockoutEnabledException()
        {
        }

        public UserSetLockoutEnabledException(string message) : base(message)
        {
        }

        public UserSetLockoutEnabledException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public UserSetLockoutEnabledException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
