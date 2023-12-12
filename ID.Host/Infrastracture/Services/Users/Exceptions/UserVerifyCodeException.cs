using ID.Core.Exceptions.Base;
using System.Runtime.Serialization;

namespace ID.Host.Infrastracture.Services.Users.Exceptions
{
    public class UserVerifyCodeException : BaseIDException
    {
        public UserVerifyCodeException()
        {
        }

        public UserVerifyCodeException(string message) : base(message)
        {
        }

        public UserVerifyCodeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public UserVerifyCodeException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
