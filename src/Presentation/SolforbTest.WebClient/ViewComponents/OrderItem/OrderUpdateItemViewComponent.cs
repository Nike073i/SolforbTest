using MediatR;
using Microsoft.AspNetCore.Mvc;
using SolforbTest.Application.OrderItems.Queries.GetOrderItem;
using SolforbTest.WebClient.Models.BindingModels;

namespace SolforbTest.WebClient.ViewComponents.OrderItem
{
    public class OrderUpdateItemViewComponent : ViewComponent
    {
        private readonly IMediator _mediator;

        public OrderUpdateItemViewComponent(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync(int orderId, int orderItemId)
        {
            var orderItem = await _mediator.Send(new GetOrderItemQuery(orderItemId));

            return View(
                "/Views/Shared/OrderItem/_OrderItem.cshtml",
                new OrderItemBindingModel(orderId)
                {
                    Name = orderItem.Name,
                    OrderItemId = orderItem.Id,
                    Quantity = orderItem.Quantity,
                    Unit = orderItem.Unit
                }
            );
        }
    }
}
