using ID.Core.Exceptions.Base;

namespace ID.Core.ApiResources.Exceptions
{
    public class ApiResourceEditException : BaseIDException
    {
        public ApiResourceEditException()
        {
        }

        public ApiResourceEditException(string message) : base(message)
        {
        }
    }
}
