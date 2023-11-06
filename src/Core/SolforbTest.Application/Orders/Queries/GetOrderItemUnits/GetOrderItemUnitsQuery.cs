using MediatR;

namespace SolforbTest.Application.Orders.Queries.GetOrderItemUnits
{
    public record GetOrderItemUnitsQuery : IRequest<IEnumerable<string>>;
}
