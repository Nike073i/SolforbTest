namespace SolforbTest.Domain
{
    public class Provider
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public Provider(string name)
        {
            Name = name;
        }
    }
}
