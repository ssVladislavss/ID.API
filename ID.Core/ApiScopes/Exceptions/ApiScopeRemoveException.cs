using ID.Core.Exceptions.Base;

namespace ID.Core.ApiScopes.Exceptions
{
    public class ApiScopeRemoveException : BaseIDException
    {
        public ApiScopeRemoveException()
        {
        }

        public ApiScopeRemoveException(string message) : base(message)
        {
        }
    }
}
