using ID.Core.Exceptions.Base;

namespace ID.Core.Roles.Exceptions
{
    public class RoleEditException : BaseIDException
    {
        public RoleEditException()
        {
        }

        public RoleEditException(string message) : base(message)
        {
        }
    }
}
