using ID.Core.Exceptions.Base;

namespace ID.Core.ApiScopes.Exceptions
{
    public class ApiScopeAddException : BaseIDException
    {
        public ApiScopeAddException()
        {
        }

        public ApiScopeAddException(string message) : base(message)
        {
        }
    }
}
