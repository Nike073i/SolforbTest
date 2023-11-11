using MediatR;
using Microsoft.AspNetCore.Mvc;
using SolforbTest.Application.Orders.Queries.GetOrderDetail;
using SolforbTest.WebClient.Models.ViewModels;

namespace SolforbTest.WebClient.ViewComponents.Order
{
    public class OrderDetailsViewComponent : ViewComponent
    {
        private readonly IMediator _mediator;

        public OrderDetailsViewComponent(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync(int orderId)
        {
            var orderDetails = await _mediator.Send(new GetOrderDetailQuery(orderId));
            return View(
                "/Views/Shared/Order/_OrderDetailsComponent.cshtml",
                new OrderDetailsComponentViewModel(
                    orderDetails.Number,
                    new OrderInfoViewModel(
                        orderDetails.OrderId,
                        orderDetails.Number,
                        orderDetails.ProviderName,
                        orderDetails.Date
                    ),
                    new OrderItemsViewModel(orderDetails.OrderId, orderDetails.OrderItems)
                )
            );
        }
    }
}
