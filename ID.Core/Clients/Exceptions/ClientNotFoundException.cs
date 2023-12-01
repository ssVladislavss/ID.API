using ID.Core.Exceptions.Base;

namespace ID.Core.Clients.Exceptions
{
    public class ClientNotFoundException : BaseIDException
    {
        public ClientNotFoundException()
        {
        }

        public ClientNotFoundException(string message) : base(message)
        {
        }
    }
}
