using SolforbTest.Application.OrderItems.Queries.GetOrderItemUnits;
using SolforbTest.Core.Tests.Common;
using SolforbTest.EfContext.Context;

namespace SolforbTest.Core.Tests.OrderItems.Queries
{
    [Collection(nameof(QueryCollection))]
    public class GetOrderItemUnitsQueryHandlerTests
    {
        private readonly SolforbDbContext _context;

        public GetOrderItemUnitsQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task GetOrderItemUnitsQueryHandlerTests_Success()
        {
            var handler = new GetOrderItemUnitsQueryHandler(_context);

            var result = await handler.Handle(new GetOrderItemUnitsQuery(), CancellationToken.None);

            var units = TestData.OrderTestList.SelectMany(
                o => o.OrderItems?.Select(i => i.Unit) ?? Enumerable.Empty<string>()
            );
            var uniqueUnits = units.Distinct();
            result.Count().ShouldBe(uniqueUnits.Count());
        }
    }
}
