using ID.Core.Exceptions.Base;

namespace ID.Core.Users.Exceptions
{
    public class UserNotFoundException : BaseIDException
    {
        public UserNotFoundException()
        {
        }

        public UserNotFoundException(string message) : base(message)
        {
        }
    }
}
