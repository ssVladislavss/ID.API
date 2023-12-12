using ID.Core.Exceptions.Base;
using System.Runtime.Serialization;

namespace ID.Host.Infrastracture.Services.Users.Exceptions
{
    public class UserEmailNotifyDeliveredException : BaseIDException
    {
        public UserEmailNotifyDeliveredException()
        {
        }

        public UserEmailNotifyDeliveredException(string message) : base(message)
        {
        }

        public UserEmailNotifyDeliveredException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public UserEmailNotifyDeliveredException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
