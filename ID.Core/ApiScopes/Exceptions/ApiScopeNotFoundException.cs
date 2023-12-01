using ID.Core.Exceptions.Base;

namespace ID.Core.ApiScopes.Exceptions
{
    public class ApiScopeNotFoundException : BaseIDException
    {
        public ApiScopeNotFoundException()
        {
        }

        public ApiScopeNotFoundException(string message) : base(message)
        {
        }
    }
}
