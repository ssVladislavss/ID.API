using ID.Core.Exceptions.Base;

namespace ID.Core.Users.Exceptions
{
    public class UserEditException : BaseIDException
    {
        public UserEditException()
        {
        }

        public UserEditException(string message) : base(message)
        {
        }
    }
}
