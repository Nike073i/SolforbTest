using MediatR;

namespace SolforbTest.Application.Orders.Queries.GetOrderDetail
{
    public record GetOrderDetailQuery(int OrderId) : IRequest<OrderDetailViewModel>;
}
