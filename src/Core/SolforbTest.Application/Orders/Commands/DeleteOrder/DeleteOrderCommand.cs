using MediatR;

namespace SolforbTest.Application.Orders.Commands.DeleteOrder
{
    public record DeleteOrderCommand(int OrderId) : IRequest<int>;
}
