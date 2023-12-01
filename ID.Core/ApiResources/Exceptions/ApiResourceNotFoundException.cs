using ID.Core.Exceptions.Base;

namespace ID.Core.ApiResources.Exceptions
{
    public class ApiResourceNotFoundException : BaseIDException
    {
        public ApiResourceNotFoundException() { }
        public ApiResourceNotFoundException(string message) : base(message) { }
    }
}
