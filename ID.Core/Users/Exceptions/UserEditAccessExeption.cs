using ID.Core.Exceptions.Base;

namespace ID.Core.Users.Exceptions
{
    public class UserEditAccessExeption : BaseIDException
    {
        public UserEditAccessExeption()
        {
        }

        public UserEditAccessExeption(string message) : base(message)
        {
        }
    }
}
