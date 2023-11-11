using MediatR;
using SolforbTest.Application.OrderItems.Dto;

namespace SolforbTest.Application.OrderItems.Commands.UpdateOrderItem
{
    public record UpdateOrderItemCommand(int OrderId, int OrderItemId, OrderItemDto OrderItemDto)
        : IRequest<Unit>;
}
