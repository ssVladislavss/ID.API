using ID.Core.Exceptions.Base;

namespace ID.Core.Users.Exceptions
{
    public class UserRemoveAccessException : BaseIDException
    {
        public UserRemoveAccessException()
        {
        }

        public UserRemoveAccessException(string message) : base(message)
        {
        }
    }
}
