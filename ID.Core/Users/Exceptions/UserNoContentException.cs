using ID.Core.Exceptions.Base;

namespace ID.Core.Users.Exceptions
{
    public class UserNoContentException : BaseIDException
    {
        public UserNoContentException()
        {
        }

        public UserNoContentException(string message) : base(message)
        {
        }
    }
}
