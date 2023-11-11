using SolforbTest.Application.Orders.Queries.GetOrderList;

namespace SolforbTest.WebClient.Models.ViewModels
{
    public record OrdersComponentViewModel(
        IEnumerable<OrderViewModel> Orders,
        PaginationViewModel Pagination
    );
}
