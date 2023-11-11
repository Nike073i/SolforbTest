using MediatR;

namespace SolforbTest.Application.Orders.Commands.UpdateOrder
{
    public record UpdateOrderCommand(int OrderId, string Number, DateTime Date, int ProviderId)
        : IRequest<int>;
}
