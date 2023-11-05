namespace SolforbTest.Application.Common.Exceptions
{
    public class InvalidOrderNumberException : Exception
    {
        public InvalidOrderNumberException(string message)
            : base(message) { }
    }
}
