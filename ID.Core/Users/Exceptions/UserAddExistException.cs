using ID.Core.Exceptions.Base;

namespace ID.Core.Users.Exceptions
{
    public class UserAddExistException : BaseIDException
    {
        public UserAddExistException()
        {
        }

        public UserAddExistException(string message) : base(message)
        {
        }
    }
}
