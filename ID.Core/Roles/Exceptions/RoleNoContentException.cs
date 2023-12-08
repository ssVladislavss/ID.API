using ID.Core.Exceptions.Base;

namespace ID.Core.Roles.Exceptions
{
    public class RoleNoContentException : BaseIDException
    {
        public RoleNoContentException()
        {
        }

        public RoleNoContentException(string message) : base(message)
        {
        }
    }
}
