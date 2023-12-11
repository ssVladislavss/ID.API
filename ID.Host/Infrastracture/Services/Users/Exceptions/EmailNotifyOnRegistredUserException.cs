using ID.Core.Exceptions.Base;
using System.Runtime.Serialization;

namespace ID.Host.Infrastracture.Services.Users.Exceptions
{
    public class EmailNotifyOnRegistredUserException : BaseIDException
    {
        public EmailNotifyOnRegistredUserException()
        {
        }

        public EmailNotifyOnRegistredUserException(string message) : base(message)
        {
        }

        public EmailNotifyOnRegistredUserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public EmailNotifyOnRegistredUserException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
