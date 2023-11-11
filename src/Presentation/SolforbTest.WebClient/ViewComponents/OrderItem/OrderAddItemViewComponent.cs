using Microsoft.AspNetCore.Mvc;
using SolforbTest.WebClient.Models.BindingModels;

namespace SolforbTest.WebClient.ViewComponents.OrderItem
{
    public class OrderAddItemViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int orderId) =>
            View("/Views/Shared/OrderItem/_OrderItem.cshtml", new OrderItemBindingModel(orderId));
    }
}
