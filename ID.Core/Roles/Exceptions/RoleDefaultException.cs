using ID.Core.Exceptions.Base;

namespace ID.Core.Roles.Exceptions
{
    public class RoleDefaultException : BaseIDException
    {
        public RoleDefaultException()
        {
        }

        public RoleDefaultException(string message) : base(message)
        {
        }
    }
}
