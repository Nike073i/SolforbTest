using SolforbTest.Application.Orders.Queries.GetOrderNumbers;
using SolforbTest.Core.Tests.Common;
using SolforbTest.EfContext.Context;

namespace SolforbTest.Core.Tests.Orders.Queries
{
    [Collection(nameof(QueryCollection))]
    public class GetOrderNumbersQueryHandlerTests
    {
        private readonly SolforbDbContext _context;

        public GetOrderNumbersQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task GetOrderNumbersQueryHandlerTests_Success()
        {
            var handler = new GetOrderNumbersQueryHandler(_context);

            var result = await handler.Handle(new GetOrderNumbersQuery(), CancellationToken.None);

            result.Count().ShouldBe(TestData.OrderTestList.Count);
        }
    }
}
