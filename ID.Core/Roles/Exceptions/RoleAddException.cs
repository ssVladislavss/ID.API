using ID.Core.Exceptions.Base;

namespace ID.Core.Roles.Exceptions
{
    public class RoleAddException : BaseIDException
    {
        public RoleAddException()
        {
        }

        public RoleAddException(string message) : base(message)
        {
        }
    }
}
