using MediatR;

namespace SolforbTest.Application.Orders.Commands.UpdateOrder
{
    public record UpdateOrderCommand(int OrderId, string? Number, int? ProviderId) : IRequest<int>;
}
