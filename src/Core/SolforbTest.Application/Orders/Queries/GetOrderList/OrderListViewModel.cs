namespace SolforbTest.Application.Orders.Queries.GetOrderList
{
    public record OrderListViewModel(
        int PageNumber,
        int PageCount,
        IEnumerable<OrderViewModel> Orders
    );
}
