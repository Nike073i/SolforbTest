using MediatR;
using SolforbTest.Application.Orders.Dto;

namespace SolforbTest.Application.Orders.Commands.AddOrderItem
{
    public record AddOrderItemCommand(int OrderItemId, CreateOrderItemDto OrderItemDto)
        : IRequest<Unit>;
}
