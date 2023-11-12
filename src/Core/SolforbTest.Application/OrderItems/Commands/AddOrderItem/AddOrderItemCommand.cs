using MediatR;
using SolforbTest.Application.OrderItems.Dto;

namespace SolforbTest.Application.OrderItems.Commands.AddOrderItem
{
    public record AddOrderItemCommand(int OrderId, OrderItemDto OrderItemDto) : IRequest<int>;
}
