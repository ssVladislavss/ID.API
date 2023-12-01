using ID.Core.Exceptions.Base;

namespace ID.Core.ApiResources.Exceptions
{
    public class ApiResourceRemoveException : BaseIDException
    {
        public ApiResourceRemoveException()
        {
        }

        public ApiResourceRemoveException(string message) : base(message)
        {
        }
    }
}
