using ID.Core.Exceptions.Base;

namespace ID.Core.Users.Exceptions
{
    public class UserAddException : BaseIDException
    {
        public UserAddException()
        {
        }

        public UserAddException(string message) : base(message)
        {
        }
    }
}
