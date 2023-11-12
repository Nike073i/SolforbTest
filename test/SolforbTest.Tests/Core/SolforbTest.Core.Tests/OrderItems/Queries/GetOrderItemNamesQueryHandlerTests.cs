using SolforbTest.Application.OrderItems.Queries.GetOrderItemNames;
using SolforbTest.Core.Tests.Common;
using SolforbTest.EfContext.Context;

namespace SolforbTest.Core.Tests.OrderItems.Queries
{
    [Collection(nameof(QueryCollection))]
    public class GetOrderItemNamesQueryHandlerTests
    {
        private readonly SolforbDbContext _context;

        public GetOrderItemNamesQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task GetOrderItemNamesQueryHandlerTests_Success()
        {
            var handler = new GetOrderItemNamesQueryHandler(_context);

            var result = await handler.Handle(new GetOrderItemNamesQuery(), CancellationToken.None);

            var names = TestData.OrderTestList.SelectMany(
                o => o.OrderItems?.Select(i => i.Name) ?? Enumerable.Empty<string>()
            );
            var uniqueNames = names.Distinct();
            result.Count().ShouldBe(uniqueNames.Count());
        }
    }
}
