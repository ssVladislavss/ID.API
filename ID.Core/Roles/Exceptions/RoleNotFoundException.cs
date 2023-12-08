using ID.Core.Exceptions.Base;

namespace ID.Core.Roles.Exceptions
{
    public class RoleNotFoundException : BaseIDException
    {
        public RoleNotFoundException()
        {
        }

        public RoleNotFoundException(string message) : base(message)
        {
        }
    }
}
