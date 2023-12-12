using ID.Core.Exceptions.Base;
using System.Runtime.Serialization;

namespace ID.Host.Infrastracture.Services.Users.Exceptions
{
    public class UserVerifyCodeExpireException : BaseIDException
    {
        public UserVerifyCodeExpireException()
        {
        }

        public UserVerifyCodeExpireException(string message) : base(message)
        {
        }

        public UserVerifyCodeExpireException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public UserVerifyCodeExpireException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
