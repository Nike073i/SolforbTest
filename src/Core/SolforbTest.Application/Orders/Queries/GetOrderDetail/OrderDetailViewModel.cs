using SolforbTest.Application.OrderItems.Dto;

namespace SolforbTest.Application.Orders.Queries.GetOrderDetail
{
    public record OrderDetailViewModel(
        int OrderId,
        string Number,
        DateTime Date,
        string ProviderName,
        int ProviderId,
        IEnumerable<OrderItemViewModel> OrderItems
    );
}
