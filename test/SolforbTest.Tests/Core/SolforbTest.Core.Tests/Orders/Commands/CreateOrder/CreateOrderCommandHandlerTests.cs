using SolforbTest.Application.Common.Exceptions;
using SolforbTest.Application.Orders.Commands.CreateOrder;
using SolforbTest.Core.Tests.Common;

namespace SolforbTest.Core.Tests.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task CreateOrderCommandHandler_Success()
        {
            var handler = new CreateOrderCommandHandler(Context);
            int providerId = 1;
            string number = Guid.NewGuid().ToString();

            var newOrderId = await handler.Handle(
                new CreateOrderCommand(number, providerId),
                CancellationToken.None
            );

            newOrderId.ShouldBeGreaterThan(0);
            var storedOrder = Context.Orders.FirstOrDefault(o => o.Id == newOrderId);
            storedOrder.ShouldNotBeNull();
            storedOrder.Number.ShouldBe(number);
            storedOrder.ProviderId.ShouldBe(providerId);
            storedOrder.Date.ShouldBeInRange(new DateTime(), DateTime.UtcNow);
        }

        [Fact]
        public async Task CreateOrderCommandHandler_ProviderAlreadyHaveOrder_ThrowAlreadyExists()
        {
            var handler = new CreateOrderCommandHandler(Context);
            int providerId = TestData.OrderTestList[0].ProviderId;
            string number = TestData.OrderTestList[0].Number;

            await Assert.ThrowsAsync<AlreadyExistException>(
                async () =>
                    await handler.Handle(
                        new CreateOrderCommand(number, providerId),
                        CancellationToken.None
                    )
            );
        }
    }
}
