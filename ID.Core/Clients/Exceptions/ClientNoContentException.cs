using ID.Core.Exceptions.Base;

namespace ID.Core.Clients.Exceptions
{
    public class ClientNoContentException : BaseIDException
    {
        public ClientNoContentException()
        {
        }

        public ClientNoContentException(string message) : base(message)
        {
        }
    }
}
