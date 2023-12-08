using ID.Core.Exceptions.Base;

namespace ID.Core.Roles.Exceptions
{
    public class RoleRemoveException : BaseIDException
    {
        public RoleRemoveException()
        {
        }

        public RoleRemoveException(string message) : base(message)
        {
        }
    }
}
