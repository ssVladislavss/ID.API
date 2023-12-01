using ID.Core.Exceptions.Base;

namespace ID.Core.Clients.Exceptions
{
    public class ClientEditException : BaseIDException
    {
        public ClientEditException()
        {
        }

        public ClientEditException(string message) : base(message)
        {
        }
    }
}
