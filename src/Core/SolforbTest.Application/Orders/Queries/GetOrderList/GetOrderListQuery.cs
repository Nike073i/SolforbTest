using MediatR;
using SolforbTest.Application.Common.Options;
using SolforbTest.Application.Orders.Options;

namespace SolforbTest.Application.Orders.Queries.GetOrderList
{
    public record GetOrderListQuery(
        OrderFilterOptions? FilterOptions = null,
        PaginationOptions? PaginationOptions = null,
        OrderSortOptions? SortOptions = null
    ) : IRequest<OrderListViewModel>;
}
