using MediatR;

namespace SolforbTest.Application.OrderItems.Commands.RemoveOrderItem
{
    public record RemoveOrderItemCommand(int OrderId, int OrderItemId) : IRequest<Unit>;
}
