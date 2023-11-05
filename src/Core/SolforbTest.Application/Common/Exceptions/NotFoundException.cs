namespace SolforbTest.Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object id)
            : base($"Сущность \"{name}\"({id}) не найдена") { }
    }
}
