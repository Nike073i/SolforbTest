using SolforbTest.Application.Providers.Queries.GetProviderList;
using SolforbTest.Core.Tests.Common;
using SolforbTest.EfContext.Context;

namespace SolforbTest.Core.Tests.Providers.Queries
{
    [Collection(nameof(QueryCollection))]
    public class GetProviderListQueryHandlerTests
    {
        private readonly SolforbDbContext _context;

        public GetProviderListQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task GetProviderListQueryHandler_Success()
        {
            var handler = new GetProviderListQueryHandler(_context);

            var result = await handler.Handle(new GetProviderListQuery(), CancellationToken.None);

            result.ShouldBeOfType<ProvidersListViewModel>();

            // Поставщики должны быть заполнены изначально. Их количество указано в конфигурации контекста
            result.Providers.Count().ShouldBe(5);
        }
    }
}
