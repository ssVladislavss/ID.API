using ID.Core.Exceptions.Base;

namespace ID.Core.ApiScopes.Exceptions
{
    public class ApiScopeNoContentException : BaseIDException
    {
        public ApiScopeNoContentException()
        {
        }

        public ApiScopeNoContentException(string message) : base(message)
        {
        }
    }
}
