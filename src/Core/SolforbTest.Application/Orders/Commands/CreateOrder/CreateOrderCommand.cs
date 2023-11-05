using MediatR;
using SolforbTest.Application.Orders.Dto;

namespace SolforbTest.Application.Orders.Commands.CreateOrder
{
    public record CreateOrderCommand(
        string Number,
        int ProviderId,
        IEnumerable<CreateOrderItemDto> OrderItems
    ) : IRequest<int>;
}
