namespace SolforbTest.Domain
{
    public class Provider
    {
        public int Id { get; init; }

        public string Name { get; }

        public Provider(string name)
        {
            Name = name;
        }
    }
}
