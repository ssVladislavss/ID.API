using ID.Core.Exceptions.Base;

namespace ID.Core.ApiResources.Exceptions
{
    public class ApiResourceNoContentException : BaseIDException
    {
        public ApiResourceNoContentException() { }
        public ApiResourceNoContentException(string message) : base(message) { }
    }
}
