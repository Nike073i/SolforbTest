using MediatR;
using SolforbTest.Application.Orders.Dto;

namespace SolforbTest.Application.Orders.Commands.AddOrderItem
{
    public record AddOrderItemCommand(int OrderId, CreateOrderItemDto OrderItemDto)
        : IRequest<Unit>;
}
