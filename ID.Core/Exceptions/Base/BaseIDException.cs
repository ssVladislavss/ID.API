namespace ID.Core.Exceptions.Base
{
    public class BaseIDException : Exception
    {
        public BaseIDException() { }
        public BaseIDException(string message) : base(message) { }
    }
}
