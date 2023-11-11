using MediatR;
using SolforbTest.Application.OrderItems.Dto;

namespace SolforbTest.Application.OrderItems.Queries.GetOrderItem
{
    public record GetOrderItemQuery(int OrderItemId) : IRequest<OrderItemViewModel>;
}
