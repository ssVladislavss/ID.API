using ID.Core.Exceptions.Base;

namespace ID.Core.Clients.Exceptions
{
    public class ClientDefaultException : BaseIDException
    {
        public ClientDefaultException()
        {
        }

        public ClientDefaultException(string message) : base(message)
        {
        }
    }
}
