using ID.Core.Exceptions.Base;

namespace ID.Core.Clients.Exceptions
{
    public class ClientRemoveAccessException : BaseIDException
    {
        public ClientRemoveAccessException()
        {
        }

        public ClientRemoveAccessException(string message) : base(message)
        {
        }
    }
}
