using ID.Core.Exceptions.Base;

namespace ID.Core.Clients.Exceptions
{
    public class ClientRemoveException : BaseIDException
    {
        public ClientRemoveException()
        {
        }

        public ClientRemoveException(string message) : base(message)
        {
        }
    }
}
