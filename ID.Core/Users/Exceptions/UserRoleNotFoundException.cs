using ID.Core.Exceptions.Base;

namespace ID.Core.Users.Exceptions
{
    public class UserRoleNotFoundException : BaseIDException
    {
        public UserRoleNotFoundException()
        {
        }

        public UserRoleNotFoundException(string message) : base(message)
        {
        }
    }
}
