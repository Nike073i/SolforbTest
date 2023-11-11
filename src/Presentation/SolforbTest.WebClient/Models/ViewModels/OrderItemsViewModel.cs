using SolforbTest.Application.OrderItems.Dto;

namespace SolforbTest.WebClient.Models.ViewModels
{
    public record OrderItemsViewModel(
        int OrderId,
        IEnumerable<OrderItemViewModel> OrderItems,
        bool IsEditable = false
    );
}
