using SolforbTest.EfContext.Context;

namespace SolforbTest.Core.Tests.Common
{
    public class QueryTestFixture : IDisposable
    {
        public SolforbDbContext Context { get; }

        public QueryTestFixture()
        {
            Context = SolforbDbContextFactory.Create();
        }

        public void Dispose() => SolforbDbContextFactory.Destroy(Context);
    }

    [CollectionDefinition(nameof(QueryCollection))]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}
