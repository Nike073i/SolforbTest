using Microsoft.EntityFrameworkCore;
using SolforbTest.Application.Common.Exceptions;
using SolforbTest.Application.OrderItems.Commands.RemoveOrderItem;
using SolforbTest.Core.Tests.Common;

namespace SolforbTest.Core.Tests.OrderItems.Commands.RemoveOrderItem
{
    public class RemoveOrderItemCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task RemoveOrderItemCommandHandler_Success()
        {
            var handler = new RemoveOrderItemCommandHandler(Context);
            var orderIdForDelete = TestData.TestOrderId;
            var orderItemIdForDelete = TestData.TestOrderItemId;

            await handler.Handle(
                new RemoveOrderItemCommand(orderIdForDelete, orderItemIdForDelete),
                CancellationToken.None
            );

            var order = await Context.Orders.SingleOrDefaultAsync(o => o.Id == orderIdForDelete);
            order.ShouldNotBeNull();
            order.OrderItems!.ShouldNotContain(i => i.Id == orderItemIdForDelete);
        }

        [Fact]
        public async Task RemoveOrderItemCommandHandler_FailOnWrongOrderId()
        {
            var handler = new RemoveOrderItemCommandHandler(Context);
            var wrongOrderId = 5000;
            var orderItemIdForDelete = TestData.TestOrderItemId;

            await Assert.ThrowsAsync<NotFoundException>(
                async () =>
                    await handler.Handle(
                        new RemoveOrderItemCommand(wrongOrderId, orderItemIdForDelete),
                        CancellationToken.None
                    )
            );
        }

        [Fact]
        public async Task RemoveOrderItemCommandHandler_FailOnWrongOrderItemId()
        {
            var handler = new RemoveOrderItemCommandHandler(Context);
            var orderIdForDelete = TestData.TestOrderId;
            var wrongOrderItemIdForDelete = 5000;

            await Assert.ThrowsAsync<NotFoundException>(
                async () =>
                    await handler.Handle(
                        new RemoveOrderItemCommand(orderIdForDelete, wrongOrderItemIdForDelete),
                        CancellationToken.None
                    )
            );
        }
    }
}
