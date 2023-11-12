using SolforbTest.Application.Common.Exceptions;
using SolforbTest.Application.Orders.Commands.DeleteOrder;
using SolforbTest.Core.Tests.Common;

namespace SolforbTest.Core.Tests.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task DeleteOrderCommandHandler_Success()
        {
            var handler = new DeleteOrderCommandHandler(Context);
            var orderForDelete = TestData.OrderTestList[0];
            var orderId = orderForDelete.Id;
            var orderItemIds = orderForDelete.OrderItems!.Select(x => x.Id).ToList();

            await handler.Handle(new DeleteOrderCommand(orderId), CancellationToken.None);

            Assert.False(Context.Orders.Any(o => o.Id == orderId));
            Assert.False(Context.OrderItems.ToList().IntersectBy(orderItemIds, i => i.Id).Any());
        }

        [Fact]
        public async Task DeleteOrderCommandHandler_FailOnWrongId()
        {
            var handler = new DeleteOrderCommandHandler(Context);
            var orderId = 5000;

            await Assert.ThrowsAsync<NotFoundException>(
                async () =>
                    await handler.Handle(new DeleteOrderCommand(orderId), CancellationToken.None)
            );
        }
    }
}
