using MediatR;

namespace SolforbTest.Application.OrderItems.Queries.GetOrderItemUnits
{
    public record GetOrderItemUnitsQuery : IRequest<IEnumerable<string>>;
}
