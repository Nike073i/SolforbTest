using SolforbTest.Application.Common.Exceptions;
using SolforbTest.Application.Orders.Queries.GetOrderDetail;
using SolforbTest.Core.Tests.Common;
using SolforbTest.EfContext.Context;

namespace SolforbTest.Core.Tests.Orders.Queries
{
    [Collection(nameof(QueryCollection))]
    public class GetOrderDetailsQueryHandlerTests
    {
        private readonly SolforbDbContext _context;

        public GetOrderDetailsQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task GetOrderDetailsQueryHandlerTests_Success()
        {
            int orderId = TestData.OrderTestList[0].Id;
            var handler = new GetOrderDetailQueryHandler(_context);

            var result = await handler.Handle(
                new GetOrderDetailQuery(orderId),
                CancellationToken.None
            );

            result.ShouldBeOfType<OrderDetailViewModel>();

            var expectedData = TestData.OrderTestList[0];
            result.OrderId.ShouldBe(expectedData.Id);
            result.Number.ShouldBe(expectedData.Number);
            result.Date.ShouldBe(expectedData.Date);
            result.ProviderName.ShouldNotBeNull();
            result.ProviderId.ShouldBe(expectedData.ProviderId);
            result.OrderItems.ShouldNotBeNull();
            result.OrderItems.Count().ShouldBe(expectedData.OrderItems!.Count);
        }

        [Fact]
        public async Task GetOrderDetailsQueryHandlerTests_FailOnWrongId()
        {
            int wrongOrderId = 5000;
            var handler = new GetOrderDetailQueryHandler(_context);

            await Assert.ThrowsAsync<NotFoundException>(
                async () =>
                    await handler.Handle(
                        new GetOrderDetailQuery(wrongOrderId),
                        CancellationToken.None
                    )
            );
        }
    }
}
