namespace SolforbTest.WebClient.Models.ViewModels
{
    public record OrderDetailsComponentViewModel(
        string OrderNumber,
        OrderInfoViewModel OrderInfo,
        OrderItemsViewModel OrderItems
    );
}
