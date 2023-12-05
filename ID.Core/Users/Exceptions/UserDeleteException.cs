using ID.Core.Exceptions.Base;

namespace ID.Core.Users.Exceptions
{
    public class UserDeleteException : BaseIDException
    {
        public UserDeleteException()
        {
        }

        public UserDeleteException(string message) : base(message)
        {
        }
    }
}
