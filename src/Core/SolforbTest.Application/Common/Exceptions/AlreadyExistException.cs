namespace SolforbTest.Application.Common.Exceptions
{
    public class AlreadyExistException : Exception
    {
        public AlreadyExistException(string message)
            : base(message) { }
    }
}
