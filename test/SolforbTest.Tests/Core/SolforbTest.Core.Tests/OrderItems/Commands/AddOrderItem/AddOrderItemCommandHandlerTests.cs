using Microsoft.EntityFrameworkCore;
using SolforbTest.Application.Common.Exceptions;
using SolforbTest.Application.OrderItems.Commands.AddOrderItem;
using SolforbTest.Application.OrderItems.Dto;
using SolforbTest.Core.Tests.Common;

namespace SolforbTest.Core.Tests.OrderItems.Commands.AddOrderItem
{
    public class AddOrderItemCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task AddOrderItemCommandHandler_Success()
        {
            var handler = new AddOrderItemCommandHandler(Context);
            var orderId = TestData.TestOrderId;
            var orderItemDto = CreateDto();
            var orderItemId = await handler.Handle(
                new AddOrderItemCommand(orderId, orderItemDto),
                CancellationToken.None
            );

            var storedOrder = await Context.Orders.SingleOrDefaultAsync(o => o.Id == orderId);
            storedOrder.ShouldNotBeNull();
            var storedOrderItem = storedOrder.OrderItems!.FirstOrDefault(i => i.Id == orderItemId);
            storedOrderItem.ShouldNotBeNull();
            storedOrderItem.Name.ShouldBe(orderItemDto.Name);
            storedOrderItem.Unit.ShouldBe(orderItemDto.Unit);
            storedOrderItem.Quantity.ShouldBe(orderItemDto.Quantity);
        }

        [Fact]
        public async Task AddOrderItemCommandHandler_FailOnWrongOrderId()
        {
            var handler = new AddOrderItemCommandHandler(Context);
            var orderId = 500;
            var orderItemDto = CreateDto();
            await Assert.ThrowsAsync<NotFoundException>(
                async () =>
                    await handler.Handle(
                        new AddOrderItemCommand(orderId, orderItemDto),
                        CancellationToken.None
                    )
            );
        }

        [Fact]
        public async Task AddOrderItemCommandHandler_FailByOrderAlreadyHaveName()
        {
            var handler = new AddOrderItemCommandHandler(Context);
            var orderId = TestData.TestOrderId;
            var orderItemDto = CreateDto(name: TestData.TestOrderItemName);
            await Assert.ThrowsAsync<AlreadyExistException>(
                async () =>
                    await handler.Handle(
                        new AddOrderItemCommand(orderId, orderItemDto),
                        CancellationToken.None
                    )
            );
        }

        [Fact]
        public async Task AddOrderItemCommandHandler_FailByOrderNumberEqualsName()
        {
            var handler = new AddOrderItemCommandHandler(Context);
            var orderId = TestData.TestOrderId;
            var orderItemDto = CreateDto(name: TestData.TestOrderNumber);
            await Assert.ThrowsAsync<InvalidOrderNumberException>(
                async () =>
                    await handler.Handle(
                        new AddOrderItemCommand(orderId, orderItemDto),
                        CancellationToken.None
                    )
            );
        }

        private static OrderItemDto CreateDto(
            string? name = null,
            decimal quantity = 15,
            string unit = "kg"
        ) => new(name ?? Guid.NewGuid().ToString(), quantity, unit);
    }
}
