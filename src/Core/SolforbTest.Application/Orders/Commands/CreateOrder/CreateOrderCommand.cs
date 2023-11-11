using MediatR;

namespace SolforbTest.Application.Orders.Commands.CreateOrder
{
    public record CreateOrderCommand(string Number, int ProviderId) : IRequest<int>;
}
