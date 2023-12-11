using ID.Core.Exceptions.Base;

namespace ID.Core.Users.Exceptions
{
    public class UserDefaultException : BaseIDException
    {
        public UserDefaultException()
        {
        }

        public UserDefaultException(string message) : base(message)
        {
        }
    }
}
