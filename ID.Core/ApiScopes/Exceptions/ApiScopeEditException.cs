using ID.Core.Exceptions.Base;

namespace ID.Core.ApiScopes.Exceptions
{
    public class ApiScopeEditException : BaseIDException
    {
        public ApiScopeEditException()
        {
        }

        public ApiScopeEditException(string message) : base(message)
        {
        }
    }
}
