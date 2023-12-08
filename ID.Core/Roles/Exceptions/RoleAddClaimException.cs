using ID.Core.Exceptions.Base;

namespace ID.Core.Roles.Exceptions
{
    public class RoleAddClaimException : BaseIDException
    {
        public RoleAddClaimException()
        {
        }

        public RoleAddClaimException(string message) : base(message)
        {
        }
    }
}
