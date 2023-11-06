using MediatR;

namespace SolforbTest.Application.Orders.Queries.GetOrderItemNames
{
    public record GetOrderItemNamesQuery : IRequest<IEnumerable<string>>;
}
