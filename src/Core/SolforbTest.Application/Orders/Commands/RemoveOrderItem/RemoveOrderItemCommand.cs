using MediatR;

namespace SolforbTest.Application.Orders.Commands.RemoveOrderItem
{
    public record RemoveOrderItemCommand(int OrderId, int OrderItemId) : IRequest<Unit>;
}
