using SolforbTest.Application.Common.Exceptions;
using SolforbTest.Application.OrderItems.Dto;
using SolforbTest.Application.OrderItems.Queries.GetOrderItem;
using SolforbTest.Core.Tests.Common;
using SolforbTest.EfContext.Context;

namespace SolforbTest.Core.Tests.OrderItems.Queries.GetOrderItem
{
    [Collection(nameof(QueryCollection))]
    public class GetOrderItemQueryHandlerTests
    {
        private readonly SolforbDbContext _context;

        public GetOrderItemQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task GetOrderItemQueryHandlerTests_Success()
        {
            int orderItemId = TestData.TestOrderItemId;
            var handler = new GetOrderItemQueryHandler(_context);

            var result = await handler.Handle(
                new GetOrderItemQuery(orderItemId),
                CancellationToken.None
            );

            result.ShouldBeOfType<OrderItemViewModel>();

            result.Name.ShouldBe(TestData.TestOrderItemName);
            result.Quantity.ShouldBe(TestData.TestOrderItemQuantity);
            result.Unit.ShouldBe(TestData.TestOrderItemUnit);
        }

        [Fact]
        public async Task GetOrderItemQueryHandlerTests_FailOnWrongId()
        {
            int wrongId = 5000;
            var handler = new GetOrderItemQueryHandler(_context);

            await Assert.ThrowsAsync<NotFoundException>(
                async () =>
                    await handler.Handle(new GetOrderItemQuery(wrongId), CancellationToken.None)
            );
        }
    }
}
