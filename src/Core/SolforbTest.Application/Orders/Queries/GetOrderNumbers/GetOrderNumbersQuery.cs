using MediatR;

namespace SolforbTest.Application.Orders.Queries.GetOrderNumbers
{
    public record GetOrderNumbersQuery : IRequest<IEnumerable<string>>;
}
