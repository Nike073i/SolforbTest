namespace SolforbTest.Application.Orders.Queries.GetOrderDetail
{
    public record OrderDetailViewModel(
        int OrderId,
        string Number,
        DateTime Date,
        string ProviderName,
        IEnumerable<OrderItemViewModel> OrderItems
    );
}
