using ID.Core.Exceptions.Base;
using System.Runtime.Serialization;

namespace ID.Host.Infrastracture.Services.Users.Exceptions
{
    public class UserCodeAddException : BaseIDException
    {
        public UserCodeAddException()
        {
        }

        public UserCodeAddException(string message) : base(message)
        {
        }

        public UserCodeAddException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public UserCodeAddException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
