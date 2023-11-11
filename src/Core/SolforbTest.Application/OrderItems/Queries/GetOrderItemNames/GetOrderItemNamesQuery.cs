using MediatR;

namespace SolforbTest.Application.OrderItems.Queries.GetOrderItemNames
{
    public record GetOrderItemNamesQuery : IRequest<IEnumerable<string>>;
}
