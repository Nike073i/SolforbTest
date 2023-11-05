using MediatR;

namespace SolforbTest.Application.Orders.Commands.UpdateOrder
{
    public record UpdateOrderCommand(
        int OrderId,
        string? Number = null,
        DateTime? Date = null,
        int? ProviderId = null
    ) : IRequest<int>;
}
