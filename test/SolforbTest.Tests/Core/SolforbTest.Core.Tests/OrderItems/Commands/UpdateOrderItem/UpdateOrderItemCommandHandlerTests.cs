using Microsoft.EntityFrameworkCore;
using SolforbTest.Application.Common.Exceptions;
using SolforbTest.Application.OrderItems.Commands.UpdateOrderItem;
using SolforbTest.Application.OrderItems.Dto;
using SolforbTest.Core.Tests.Common;

namespace SolforbTest.Core.Tests.OrderItems.Commands.UpdateOrderItem
{
    public class UpdateOrderItemCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task UpdateOrderItemCommandHandler_ChangeUnit_Success()
        {
            var handler = new UpdateOrderItemCommandHandler(Context);
            int orderId = TestData.TestOrderId;
            int orderItemId = TestData.TestOrderItemId;
            var orderItemDto = CreateDto(unit: Guid.NewGuid().ToString());
            await handler.Handle(
                new UpdateOrderItemCommand(orderId, orderItemId, orderItemDto),
                CancellationToken.None
            );

            await CheckUpdateResult(orderItemDto, orderId, orderItemId);
        }

        [Fact]
        public async Task UpdateOrderItemCommandHandler_ChangeQuantity_Success()
        {
            var handler = new UpdateOrderItemCommandHandler(Context);
            int orderId = TestData.TestOrderId;
            int orderItemId = TestData.TestOrderItemId;
            var orderItemDto = CreateDto(quantity: 10000m);
            await handler.Handle(
                new UpdateOrderItemCommand(orderId, orderItemId, orderItemDto),
                CancellationToken.None
            );

            await CheckUpdateResult(orderItemDto, orderId, orderItemId);
        }

        [Fact]
        public async Task UpdateOrderItemCommandHandler_ChangeName_Success()
        {
            var handler = new UpdateOrderItemCommandHandler(Context);
            int orderId = TestData.TestOrderId;
            int orderItemId = TestData.TestOrderItemId;
            var orderItemDto = CreateDto(name: Guid.NewGuid().ToString());
            await handler.Handle(
                new UpdateOrderItemCommand(orderId, orderItemId, orderItemDto),
                CancellationToken.None
            );

            await CheckUpdateResult(orderItemDto, orderId, orderItemId);
        }

        [Fact]
        public async Task UpdateOrderItemCommandHandler_ChangeName_FailByNameEqualsOrderNumber()
        {
            var handler = new UpdateOrderItemCommandHandler(Context);
            int orderId = TestData.TestOrderId;
            int orderItemId = TestData.TestOrderItemId;
            var orderItemDto = CreateDto(name: TestData.TestOrderNumber);
            await Assert.ThrowsAsync<InvalidOrderNumberException>(
                async () =>
                    await handler.Handle(
                        new UpdateOrderItemCommand(orderId, orderItemId, orderItemDto),
                        CancellationToken.None
                    )
            );
        }

        [Fact]
        public async Task UpdateOrderItemCommandHandler_ChangeName_FailByNameAlreadyExists()
        {
            var handler = new UpdateOrderItemCommandHandler(Context);
            int orderId = TestData.TestOrderId;
            int orderItemId = TestData.TestOrderItemId;
            var orderItemDto = CreateDto(name: TestData.TestOrderItemName2);
            await Assert.ThrowsAsync<AlreadyExistException>(
                async () =>
                    await handler.Handle(
                        new UpdateOrderItemCommand(orderId, orderItemId, orderItemDto),
                        CancellationToken.None
                    )
            );
        }

        [Fact]
        public async Task UpdateOrderItemCommandHandler_FailOnWrongOrderId()
        {
            var handler = new UpdateOrderItemCommandHandler(Context);
            var wrongOrderId = 500;
            int orderItemId = TestData.TestOrderItemId;
            var orderItemDto = CreateDto();
            await Assert.ThrowsAsync<NotFoundException>(
                async () =>
                    await handler.Handle(
                        new UpdateOrderItemCommand(wrongOrderId, orderItemId, orderItemDto),
                        CancellationToken.None
                    )
            );
        }

        [Fact]
        public async Task UpdateOrderItemCommandHandler_FailOnWrongOrderItemId()
        {
            var handler = new UpdateOrderItemCommandHandler(Context);
            var orderId = TestData.TestOrderId;
            int wrongOrderItemId = 5000;
            var orderItemDto = CreateDto();
            await Assert.ThrowsAsync<NotFoundException>(
                async () =>
                    await handler.Handle(
                        new UpdateOrderItemCommand(orderId, wrongOrderItemId, orderItemDto),
                        CancellationToken.None
                    )
            );
        }

        private async Task CheckUpdateResult(
            OrderItemDto expectedData,
            int orderId,
            int orderItemId
        )
        {
            var storedOrder = await Context.Orders.SingleOrDefaultAsync(o => o.Id == orderId);
            storedOrder.ShouldNotBeNull();
            var storedOrderItem = storedOrder.OrderItems!.FirstOrDefault(i => i.Id == orderItemId);
            storedOrderItem.ShouldNotBeNull();
            storedOrderItem.Name.ShouldBe(expectedData.Name);
            storedOrderItem.Unit.ShouldBe(expectedData.Unit);
            storedOrderItem.Quantity.ShouldBe(expectedData.Quantity);
        }

        private static OrderItemDto CreateDto(
            string? name = null,
            decimal? quantity = null,
            string? unit = null
        ) =>
            new(
                name ?? TestData.TestOrderItemName,
                quantity ?? TestData.TestOrderItemQuantity,
                unit ?? TestData.TestOrderItemUnit
            );
    }
}
