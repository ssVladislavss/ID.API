namespace ID.Core.Exceptions.Base
{
    public abstract class BaseIDException : Exception
    {
        public BaseIDException() { }
        public BaseIDException(string message) : base(message) { }
    }
}
