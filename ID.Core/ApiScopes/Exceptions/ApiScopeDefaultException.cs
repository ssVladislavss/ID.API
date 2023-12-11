using ID.Core.Exceptions.Base;

namespace ID.Core.ApiScopes.Exceptions
{
    public class ApiScopeDefaultException : BaseIDException
    {
        public ApiScopeDefaultException()
        {
        }

        public ApiScopeDefaultException(string message) : base(message)
        {
        }
    }
}
