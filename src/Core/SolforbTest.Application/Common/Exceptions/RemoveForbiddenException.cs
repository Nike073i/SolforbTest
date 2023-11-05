namespace SolforbTest.Application.Common.Exceptions
{
    public class RemoveForbiddenException : Exception
    {
        public RemoveForbiddenException(string name, object id, string message)
            : base($"Удаление \"{name}\" ({id}) запрещено по причине: \"{message}\"") { }
    }
}
