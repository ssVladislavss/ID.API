using ID.Core.Exceptions.Base;

namespace ID.Core.ApiResources.Exceptions
{
    public class ApiResourceAddException : BaseIDException
    {
        public ApiResourceAddException()
        {
        }

        public ApiResourceAddException(string message) : base(message)
        {
        }
    }
}
