using ID.Core.Exceptions.Base;

namespace ID.Core.ApiResources.Exceptions
{
    public class ApiResourceDefaultException : BaseIDException
    {
        public ApiResourceDefaultException()
        {
        }

        public ApiResourceDefaultException(string message) : base(message)
        {
        }
    }
}
