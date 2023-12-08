using ID.Core.Exceptions.Base;

namespace ID.Core.Roles.Exceptions
{
    public class RoleAddClaimExistException : BaseIDException
    {
        public RoleAddClaimExistException()
        {
        }

        public RoleAddClaimExistException(string message) : base(message)
        {
        }
    }
}
