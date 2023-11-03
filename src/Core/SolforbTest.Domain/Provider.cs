namespace SolforbTest.Domain
{
    public class Provider
    {
        public int Id { get; private set; }

        public string Name { get; }

        public Provider(string name)
        {
            Name = name;
        }
    }
}
