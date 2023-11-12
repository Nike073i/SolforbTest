using SolforbTest.Application.Common.Exceptions;
using SolforbTest.Application.Orders.Commands.UpdateOrder;
using SolforbTest.Core.Tests.Common;

namespace SolforbTest.Core.Tests.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task UpdateOrderCommandHandler_UpdateNumber_Success()
        {
            var handler = new UpdateOrderCommandHandler(Context);
            var orderForUpdate = TestData.OrderTestList[0];
            var orderId = orderForUpdate.Id;
            var newNumber = Guid.NewGuid().ToString();

            await handler.Handle(
                new UpdateOrderCommand(
                    orderId,
                    newNumber,
                    orderForUpdate.Date,
                    orderForUpdate.ProviderId
                ),
                CancellationToken.None
            );

            var storedOrder = Context.Orders.FirstOrDefault(o => o.Id == orderId);
            storedOrder.ShouldNotBeNull();
            storedOrder.Number.ShouldBe(newNumber);
            storedOrder.Date.ShouldBe(orderForUpdate.Date);
            storedOrder.ProviderId.ShouldBe(orderForUpdate.ProviderId);
        }

        [Fact]
        public async Task UpdateOrderCommandHandler_UpdateProviderId_Success()
        {
            var handler = new UpdateOrderCommandHandler(Context);
            var orderForUpdate = TestData.OrderTestList[0];
            var orderId = orderForUpdate.Id;
            var newProviderId = 3;

            await handler.Handle(
                new UpdateOrderCommand(
                    orderId,
                    orderForUpdate.Number,
                    orderForUpdate.Date,
                    newProviderId
                ),
                CancellationToken.None
            );

            var storedOrder = Context.Orders.FirstOrDefault(o => o.Id == orderId);
            storedOrder.ShouldNotBeNull();
            storedOrder.Number.ShouldBe(orderForUpdate.Number);
            storedOrder.Date.ShouldBe(orderForUpdate.Date);
            storedOrder.ProviderId.ShouldBe(newProviderId);
        }

        [Fact]
        public async Task UpdateOrderCommandHandler_UpdateDate_Success()
        {
            var handler = new UpdateOrderCommandHandler(Context);
            var orderForUpdate = TestData.OrderTestList[0];
            var orderId = orderForUpdate.Id;
            var newDate = DateTime.UtcNow;

            await handler.Handle(
                new UpdateOrderCommand(
                    orderId,
                    orderForUpdate.Number,
                    newDate,
                    orderForUpdate.ProviderId
                ),
                CancellationToken.None
            );

            var storedOrder = Context.Orders.FirstOrDefault(o => o.Id == orderId);
            storedOrder.ShouldNotBeNull();
            storedOrder.Number.ShouldBe(orderForUpdate.Number);
            storedOrder.Date.ShouldBe(newDate);
            storedOrder.ProviderId.ShouldBe(orderForUpdate.ProviderId);
        }

        [Fact]
        public async Task UpdateOrderCommandHandler_FailOnWrongOrderId()
        {
            var handler = new UpdateOrderCommandHandler(Context);
            var orderIdForUpdate = 5000;
            var newNumber = Guid.NewGuid().ToString();
            var newProviderId = 3;
            var newDate = DateTime.UtcNow;

            await Assert.ThrowsAsync<NotFoundException>(
                async () =>
                    await handler.Handle(
                        new UpdateOrderCommand(orderIdForUpdate, newNumber, newDate, newProviderId),
                        CancellationToken.None
                    )
            );
        }

        [Fact]
        public async Task UpdateOrderCommandHandler_ChangeNumber_FailByProviderAlreadyHaveOrder()
        {
            var handler = new UpdateOrderCommandHandler(Context);
            var orderForUpdate = TestData.OrderTestList[0];
            var newNumber = "Number 5";

            await Assert.ThrowsAsync<AlreadyExistException>(
                async () =>
                    await handler.Handle(
                        new UpdateOrderCommand(
                            orderForUpdate.Id,
                            newNumber,
                            orderForUpdate.Date,
                            orderForUpdate.ProviderId
                        ),
                        CancellationToken.None
                    )
            );
        }

        [Fact]
        public async Task UpdateOrderCommandHandler_ChangeProvider_FailByProviderAlreadyHaveOrder()
        {
            var handler = new UpdateOrderCommandHandler(Context);
            var orderForUpdate = TestData.OrderTestList[0];
            var newProviderId = 2;

            await Assert.ThrowsAsync<AlreadyExistException>(
                async () =>
                    await handler.Handle(
                        new UpdateOrderCommand(
                            orderForUpdate.Id,
                            orderForUpdate.Number,
                            orderForUpdate.Date,
                            newProviderId
                        ),
                        CancellationToken.None
                    )
            );
        }

        [Fact]
        public async Task UpdateOrderCommandHandler_ChangeNumber_FailByNumberEqualsOrderItemName()
        {
            var handler = new UpdateOrderCommandHandler(Context);
            var orderForUpdate = TestData.OrderTestList[0];
            var newNumber = "Name 1";

            await Assert.ThrowsAsync<InvalidOrderNumberException>(
                async () =>
                    await handler.Handle(
                        new UpdateOrderCommand(
                            orderForUpdate.Id,
                            newNumber,
                            orderForUpdate.Date,
                            orderForUpdate.ProviderId
                        ),
                        CancellationToken.None
                    )
            );
        }
    }
}
