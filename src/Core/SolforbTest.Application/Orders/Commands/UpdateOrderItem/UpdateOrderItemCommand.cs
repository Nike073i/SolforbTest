using MediatR;
using SolforbTest.Application.Orders.Dto;

namespace SolforbTest.Application.Orders.Commands.UpdateOrderItem
{
    public record UpdateOrderItemCommand(int OrderId, UpdateOrderItemDto OrderItemDto)
        : IRequest<Unit>;
}
