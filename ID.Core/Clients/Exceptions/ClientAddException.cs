using ID.Core.Exceptions.Base;

namespace ID.Core.Clients.Exceptions
{
    public class ClientAddException : BaseIDException
    {
        public ClientAddException()
        {
        }

        public ClientAddException(string message) : base(message)
        {
        }
    }
}
